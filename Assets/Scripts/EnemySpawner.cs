using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public Transform forbiddenPlace;
    public GameObject enemy;
    public float minDistance;
    public float maxDistance;
    public float spawnTime;
    public Transform cloneList;

    private float waitStart;
    private static readonly Vector4[] s_UnitSphere = MakeUnitSphere(16);

    private float RandomDistance()
    {
        float magnitude = Random.Range(minDistance, maxDistance);
        if (Random.value < 0.5f)
        {
            return magnitude * -1;
        }
        return magnitude;
    }

    private Vector3 PositionToSpawn()
    {
        return new Vector3(RandomDistance() + player.position.x, transform.position.y, RandomDistance() + player.position.z);
    }

    private static Vector4[] MakeUnitSphere(int len)
    {
        Debug.Assert(len > 2);
        var v = new Vector4[len * 3];
        for (int i = 0; i < len; i++)
        {
            var f = i / (float)len;
            float c = Mathf.Cos(f * (float)(Mathf.PI * 2.0));
            float s = Mathf.Sin(f * (float)(Mathf.PI * 2.0));
            v[0 * len + i] = new Vector4(c, s, 0, 1);
            v[1 * len + i] = new Vector4(0, c, s, 1);
            v[2 * len + i] = new Vector4(s, 0, c, 1);
        }
        return v;
    }

    private static void DrawSphere(Vector4 pos, float radius, Color color)
    {
        Vector4[] v = s_UnitSphere;
        int len = s_UnitSphere.Length / 3;
        for (int i = 0; i < len; i++)
        {
            var sX = pos + radius * v[0 * len + i];
            var eX = pos + radius * v[0 * len + (i + 1) % len];
            var sY = pos + radius * v[1 * len + i];
            var eY = pos + radius * v[1 * len + (i + 1) % len];
            var sZ = pos + radius * v[2 * len + i];
            var eZ = pos + radius * v[2 * len + (i + 1) % len];
            Debug.DrawLine(sX, eX, color);
            Debug.DrawLine(sY, eY, color);
            Debug.DrawLine(sZ, eZ, color);
        }
    }

    private void OnDrawGizmos()
    {
        DrawSphere(forbiddenPlace.position, minDistance, Color.red);
    }

    private void SpawnEnemy()
    {
        Vector3 possiblePosition = Vector3.zero;
        float distance = 0;
        while (distance < minDistance)
        {
            possiblePosition = PositionToSpawn();
            distance = Vector3.Distance(forbiddenPlace.position, possiblePosition);
        }
        GameObject enemySpawn = Instantiate(enemy, cloneList);
        enemySpawn.transform.position = possiblePosition;
    }

    private void Start()
    {
        waitStart = Time.time;
    }

    void Update()
    {
        if (waitStart + spawnTime < Time.time)
        {
            waitStart = Time.time;
            SpawnEnemy();
        }
    }
}

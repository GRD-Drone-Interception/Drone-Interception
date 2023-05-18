using System.Collections.Generic;
using UnityEngine;

namespace DroneMovement.Scripts.Boids
{
    public class Boid : MonoBehaviour
    {
        public float speed = 5f;
        public float rotationSpeed = 5f;

        public float neighbourDistance = 3f;

        public float separationDistance = 2f;
        public float separationWeight = 1f;

        public float alignmentWeight = 1f;
        public float cohesionWeight = 1f;

        public enum FormationType { V, Box, Diamond, Cluster };
        public FormationType currentFormation = FormationType.V;

        private void Update()
        {
            // Get neighbours
            Collider[] neighbours = Physics.OverlapSphere(transform.position, neighbourDistance);

            // Calculate movement vectors
            Vector3 separation = Vector3.zero;
            Vector3 alignment = Vector3.zero;
            Vector3 cohesion = Vector3.zero;

            foreach (Collider neighbour in neighbours)
            {
                if (neighbour.gameObject == gameObject) continue;

                // Separation
                Vector3 diff = transform.position - neighbour.transform.position;
                float distance = diff.magnitude;

                if (distance < separationDistance)
                {
                    separation += diff.normalized / distance;
                }

                // Alignment
                alignment += neighbour.transform.forward;

                // Cohesion
                cohesion += neighbour.transform.position;
            }

            // Apply weights
            Vector3 move = Vector3.zero;

            if (separation != Vector3.zero)
            {
                move += separation.normalized * separationWeight;
            }

            if (alignment != Vector3.zero)
            {
                move += alignment.normalized * alignmentWeight;
            }

            if (cohesion != Vector3.zero)
            {
                move += (cohesion / neighbours.Length - transform.position).normalized * cohesionWeight;
            }

            // Apply formation
            switch (currentFormation)
            {
                case FormationType.V:
                    move += VFormation(neighbours);
                    break;
                case FormationType.Box:
                    move += BoxFormation(neighbours);
                    break;
                /*case FormationType.Diamond:
                    move += DiamondFormation(neighbours);
                    break;
                case FormationType.Cluster:
                    move += ClusterFormation(neighbours);
                    break;*/
            }

            // Move and rotate
            transform.position += move.normalized * speed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), rotationSpeed * Time.deltaTime);
        }

        private Vector3 VFormation(Collider[] neighbours)
        {
            Vector3 move = Vector3.zero;

            // Leader flies straight ahead
            if (transform.tag == "Leader")
            {
                move += transform.forward * speed;
            }
            else
            {
                // Followers form a V shape behind the leader
                foreach (Collider neighbour in neighbours)
                {
                    if (neighbour.gameObject == gameObject) continue;

                    Vector3 toNeighbour = neighbour.transform.position - transform.position;
                    Vector3 ahead = transform.forward;

                    // Check if neighbour is within the "cone" behind the leader
                    if (Vector3.Dot(toNeighbour, ahead) > 0 && toNeighbour.magnitude < neighbourDistance / 2)
                    {
                        // Calculate position in V formation
                        Vector3 side = Vector3.Cross(ahead, Vector3.up).normalized;
                        Vector3 offset = toNeighbour.normalized * neighbourDistance / 2;
                        Vector3 formationPos = transform.position + side * offset.x + Vector3.up * offset.y;

                        move += (formationPos - neighbour.transform.position);
                    }
                }
            }

            return move;
        }

        private Vector3 BoxFormation(Collider[] neighbours)
        {
            Vector3 move = Vector3.zero;

            // Leader flies straight ahead
            if (transform.tag == "Leader")
            {
                move += transform.forward * speed;
            }
            else
            {
                // Followers form a box formation around the leader
                int numFollowers = neighbours.Length - 1;
                int numColumns = Mathf.CeilToInt(Mathf.Sqrt(numFollowers));
                int numRows = Mathf.CeilToInt((float)numFollowers / numColumns);
                float columnSpacing = neighbourDistance;
                float rowSpacing = neighbourDistance;

                Vector3 leaderPos = Vector3.zero;
                List<Vector3> followerPositions = new List<Vector3>();

                foreach (Collider neighbour in neighbours)
                {
                    if (neighbour.gameObject == gameObject) continue;

                    // Calculate the follower's position in the box formation
                    Vector3 toNeighbour = neighbour.transform.position - transform.position;
                    Vector3 side = Vector3.Cross(toNeighbour, Vector3.up).normalized;
                    Vector3 forward = Vector3.Cross(side, Vector3.up).normalized;
                    int followerIndex = followerPositions.Count;

                    int column = followerIndex % numColumns;
                    int row = Mathf.FloorToInt((float)followerIndex / numColumns);
                    Vector3 position = transform.position + forward * row * rowSpacing + side * column * columnSpacing;

                    followerPositions.Add(position);

                    if (neighbour.tag == "Leader")
                    {
                        leaderPos = position;
                    }
                }

                // Calculate the leader's position in the center of the box formation
                leaderPos = Vector3.zero;
                foreach (Vector3 followerPos in followerPositions)
                {
                    leaderPos += followerPos;
                }
                leaderPos /= followerPositions.Count;

                // Move towards the leader's position
                move += (leaderPos - transform.position);
            }

            return move;
        }

        private Vector3 ClusterFormation(Collider[] neighbours)
        {
            Vector3 move = Vector3.zero;

            // Leader flies straight ahead
            if (transform.tag == "Leader")
            {
                move += transform.forward * speed;
            }
            else
            {
                // Followers form a compact cluster formation around the leader
                int numFollowers = neighbours.Length - 1;

                // Calculate the position of the leader
                Vector3 leaderPos = Vector3.zero;
                foreach (Collider neighbour in neighbours)
                {
                    if (neighbour.gameObject == gameObject) continue;
                    if (neighbour.tag == "Leader")
                    {
                        leaderPos = neighbour.transform.position;
                        break;
                    }
                }

                // Calculate the follower positions relative to the leader
                float radius = neighbourDistance * Mathf.Sqrt(numFollowers / (2 * Mathf.PI));
                float angleIncrement = 2 * Mathf.PI / numFollowers;
                float angle = 0;

                foreach (Collider neighbour in neighbours)
                {
                    if (neighbour.gameObject == gameObject) continue;
                    if (neighbour.tag == "Leader") continue;

                    Vector3 position = leaderPos + new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
                    move += (position - transform.position);
                    angle += angleIncrement;
                }
            }

            return move;
        }

        private Vector3 DiamondFormation(Collider[] neighbours)
        {
            Vector3 move = Vector3.zero;

            // Leader flies straight ahead
            if (transform.tag == "Leader")
            {
                move += transform.forward * speed;
            }
            else
            {
                // Followers form a diamond formation around the leader
                int numFollowers = neighbours.Length - 1;
                int numColumns = Mathf.CeilToInt(Mathf.Sqrt(numFollowers));
                int numRows = Mathf.CeilToInt((float)numFollowers / numColumns);
                float columnSpacing = neighbourDistance;
                float rowSpacing = neighbourDistance;

                Vector3 leaderPos = Vector3.zero;
                List<Vector3> followerPositions = new List<Vector3>();

                foreach (Collider neighbour in neighbours)
                {
                    if (neighbour.gameObject == gameObject) continue;

                    // Calculate the follower's position in the diamond formation
                    Vector3 toNeighbour = neighbour.transform.position - transform.position;
                    Vector3 side = Vector3.Cross(toNeighbour, Vector3.up).normalized;
                    Vector3 forward = Vector3.Cross(side, Vector3.up).normalized;
                    int followerIndex = followerPositions.Count;

                    // alternate rows have one less column
                    int columnsInRow = numColumns;
                    if (followerIndex / numColumns % 2 == 1)
                    {
                        columnsInRow--;
                    }

                    int column = followerIndex % columnsInRow;
                    int row = Mathf.FloorToInt((float)followerIndex / numColumns);
                    Vector3 position = transform.position + forward * row * rowSpacing + side * (column - (columnsInRow - 1) / 2) * columnSpacing;

                    followerPositions.Add(position);

                    if (neighbour.tag == "Leader")
                    {
                        leaderPos = position;
                    }
                }

                // Calculate the leader's position in the center of the diamond formation
                leaderPos = Vector3.zero;
                foreach (Vector3 followerPos in followerPositions)
                {
                    leaderPos += followerPos;
                }
                leaderPos /= followerPositions.Count;

                // Move towards the leader's position
                move += (leaderPos - transform.position);
            }

            return move;
        }


    }
}



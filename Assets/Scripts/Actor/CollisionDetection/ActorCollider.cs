﻿using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Entity;
using System.Linq;

namespace Assets.Scripts.Actor.CollisionDetection
{
    public class ActorCollider : MonoBehaviour
    {
        public bool AssignCustomSize = false;
        public Vector2 Size;
        //[HideInInspector]
        public List<ActorCollider> OtherCollidersNear = new List<ActorCollider>();
        public BasicMaterial Material;
        //[HideInInspector]
        public List<Entity.Entity> OtherEntitiesNear = new List<Entity.Entity>();

        public void UpdateBroadEntities(Float2 Position, float distance)
        {
            OtherEntitiesNear.Clear();
            foreach (Transform T in GameObject.FindGameObjectWithTag("Ent").transform)
            {
                Vector2 conversion = new Vector2(Position.x, Position.y);
                if (Vector2.Distance(conversion, T.position) < distance)
                {
                    if (T.GetComponent<Entity.Entity>())
                    {
                        OtherEntitiesNear.Add(T.GetComponent<Entity.Entity>());
                    }
                }
            }

            OtherEntitiesNear = OtherEntitiesNear.OrderByDescending(o => Vector2.Distance(o.transform.position, new Vector2(Position.x, Position.y))).ToList();
        }

        public void Start()
        {
            if(!AssignCustomSize)
            {
                Size.x = transform.localScale.x;
                Size.y = transform.localScale.y;
            }
        }

        public void UpdateBroadCollision(Float2 Position, float distance)
        {
            OtherCollidersNear.Clear();
            foreach (Transform T in GameObject.FindGameObjectWithTag("World").transform)
            {
                Vector2 conversion = new Vector2(Position.x, Position.y);
                if (Vector2.Distance(conversion, T.position) < distance)
                {
                    if (T.GetComponent<ActorCollider>())
                    {
                        OtherCollidersNear.Add(T.GetComponent<ActorCollider>());
                    }
                }
            }
        }

        public Float2 BottomLeftPoint
        {
            get
            {
                return new Float2(transform.position.x - (Size.x / 2), transform.position.y - (Size.y / 2));
            }
            set
            {
                transform.position = new Vector2(value.x + (Size.x / 2), value.y + (Size.y / 2));
            }
        }

        public Float2 BottomRightPoint
        {
            get
            {
                return new Float2(transform.position.x + (Size.x / 2), transform.position.y - (Size.y / 2));
            }
        }

        public Float2 TopLeftPoint
        {
            get
            {
                return new Float2(transform.position.x - (Size.x / 2), transform.position.y + (Size.y / 2));
            }
        }

        public Float2 TopRightPoint
        {
            get
            {
                return new Float2(transform.position.x + (Size.x / 2), transform.position.y + (Size.y / 2));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

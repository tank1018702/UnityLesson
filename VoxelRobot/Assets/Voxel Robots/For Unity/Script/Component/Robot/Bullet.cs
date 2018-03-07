namespace MoenenGames.VoxelRobot {
	using UnityEngine;
	using UnityEditor;
	using System.Collections;


	[RequireComponent(typeof(Rigidbody))]
	public class Bullet : MonoBehaviour
    {


        public TrailRenderer t;

        #region --- VAR ---


        // Shot Cut

        public Transform Shooter
        {
			get; set;
		}

		public int Damage
        {
			get; set;
		}

		public Rigidbody Rig
        {
			get
            {
				if (!rig)
                {
					rig = GetComponent<Rigidbody>();
				}
				return rig;
			}
		}

		public Collider Col
        {
			get
            {
				if (!col)
                {
					col = GetComponent<Collider>();
				}
				return col;
			}
		}

		public Bullet Copy
        {
			get
            {
                return Instantiate(gameObject).GetComponent<Bullet>();

            }
        }


		// Serialize
		[Header("Setting")]
		[SerializeField]
		private DamageType damegeType;
		[SerializeField]
		private float LifeTime = 1f;

		[Header("On Hit")]
		[SerializeField]
		private bool DontDestroyOnHit = false;
		[SerializeField]
		private bool StopOnHit = false;

		[Header("Component")]
		[SerializeField]
		private ParticleSystem Particle;
		[SerializeField]
		private Transform Model;


		// Data
		[HideInInspector]
		public bool Alive = false;
		private Rigidbody rig = null;
		private Collider col = null;

		// Setting
		private const float BULLET_MAX_REBOUND_SPEED = 20f;



		#endregion




		#region --- MSG ---




		void Start ()
        {
			// Self Kill
			Invoke("DisableCollider", LifeTime);
			Invoke("DestoryBullet", LifeTime + 1f);
			// Size
			SetSize(transform.localScale.x);
            t = GetComponent<TrailRenderer>();
            if (t)
            {
                t.enabled = false;
            }

        }




        void OnCollisionEnter (Collision col)
        {
			if (col.collider.isTrigger)
            {
				return;
			}
			Colliding(col.transform);
		}




		void OnTriggerEnter (Collider c)
        {
			if (c.isTrigger)
            {
				return;
			}
			Colliding(c.transform);
		}




		#endregion





		#region --- LGC ---



		void Colliding (Transform tf)
        {

			if (!Alive)
            {
				return;
			}

			// Damage

			if (Shooter == tf)
            {
				return;
			}


			if (!DontDestroyOnHit)
            {

				// Logic
				Alive = false;

				// Stop
				if (StopOnHit)
                {
					Rig.velocity = Vector3.zero;
				}
                else
                {
                    //返回原向量的拷贝，并且它的模最大不超过maxLength所指示的长度。
					Rig.velocity = Vector3.ClampMagnitude(Rig.velocity, BULLET_MAX_REBOUND_SPEED);
				}

				// Particle 粒子系统
				if (Particle)
                {
					Particle.Play();
				}
                if(tf.tag=="Player"||tf.tag=="Enemy")
                {
                    if(tf.tag!=Shooter.tag)
                    {
                        tf.GetComponent<CharatherInfo>().Cost_hp(Damage);

                        
                    }
                    
                }
				// Col 碰撞器变为不可用
				DisableCollider();

				// System
                //取消该脚本上的所有延时方法
				CancelInvoke();

				DestoryBullet();

			}


		}
        private void Update()
        {
            if(t)
            {
                Debug.Log(t.enabled);
            }
            
        }


        private void DisableCollider ()
        {
			Alive = false;
			if (Col)
            {
				Col.enabled = false;
			}
		}



		private void DestoryBullet ()
        {
			Alive = false;
			TrailRenderer t = GetComponent<TrailRenderer>();
			if (t)
            {
				t.enabled = false;
			}
			if (Model)
            {
				Model.gameObject.SetActive(false);
			}

			// Destroy
			Destroy(gameObject, Particle ? Particle.main.duration + Particle.main.startLifetimeMultiplier : 0.1f);
		}


        /// <summary>
        /// 设置尾迹的宽度与质量
        /// </summary>
        /// <param name="size"></param>
		private void SetSize (float size)
        {

			// Trail
			TrailRenderer t = transform.GetComponent<TrailRenderer>();
			if (t)
            {
				t.widthMultiplier = size;
			}

			// Rig
			Rig.mass *= size;

		}



		#endregion



	}
}
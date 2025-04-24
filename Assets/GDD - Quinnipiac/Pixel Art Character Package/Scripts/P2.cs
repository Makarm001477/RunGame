using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    public class PlayerController2 : KinematicObject
    {
        public AudioClip jumpAudio2;
        public AudioClip respawnAudio2;
        public AudioClip ouchAudio2;

        public float maxSpeed2 = 7;
        public float jumpTakeOffSpeed2 = 7;

        public JumpState2 jumpState2 = JumpState2.Grounded;
        private bool stopJump2;
        public Collider2D collider2d2;
        public AudioSource audioSource2;
        public Health health2;
        public bool controlEnabled2 = true;

        bool jump2;
        Vector2 move2;
        SpriteRenderer spriteRenderer2;
        internal Animator animator2;

        readonly PlatformerModel model2 = Simulation.GetModel<PlatformerModel>();

        // ✅ เพิ่ม property public สำหรับเข้าถึง model2 จากภายนอก
        public PlatformerModel Model2 => model2;

        public Bounds Bounds2 => collider2d2.bounds;

        public int maxJumpCount2 = 2;
        private int jumpCount2 = 0;

        void Awake()
        {
            health2 = GetComponent<Health>();
            audioSource2 = GetComponent<AudioSource>();
            collider2d2 = GetComponent<Collider2D>();
            spriteRenderer2 = GetComponent<SpriteRenderer>();
            animator2 = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled2)
            {
                // ควบคุมการเคลื่อนที่ด้วย I, J, K, L
                move2.x = 0;
                if (Input.GetKey(KeyCode.J)) // สำหรับปุ่ม J (ซ้าย)
                {
                    move2.x = -1;
                }
                else if (Input.GetKey(KeyCode.L)) // สำหรับปุ่ม L (ขวา)
                {
                    move2.x = 1;
                }

                // ควบคุมการกระโดดด้วย Shift ขวา
                if (Input.GetKeyDown(KeyCode.RightShift)) // สำหรับ Shift ขวา
                {
                    if (jumpCount2 < maxJumpCount2)
                    {
                        jumpState2 = JumpState2.PrepareToJump;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.RightShift)) // เมื่อปล่อย Shift ขวา
                {
                    stopJump2 = true;
                    Schedule<PlayerStopJump2>().player = this;
                }
            }
            else
            {
                move2.x = 0;
            }

            UpdateJumpState2();
            base.Update();
        }

        void UpdateJumpState2()
        {
            jump2 = false;
            switch (jumpState2)
            {
                case JumpState2.PrepareToJump:
                    jumpState2 = JumpState2.Jumping;
                    jump2 = true;
                    stopJump2 = false;
                    jumpCount2++;
                    break;
                case JumpState2.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped2>().player = this;
                        jumpState2 = JumpState2.InFlight;
                    }
                    break;
                case JumpState2.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded2>().player = this;
                        jumpState2 = JumpState2.Landed;
                    }
                    break;
                case JumpState2.Landed:
                    jumpState2 = JumpState2.Grounded;
                    break;
                case JumpState2.Grounded:
                    jumpCount2 = 0;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump2)
            {
                velocity.y = jumpTakeOffSpeed2 * model2.jumpModifier;
                jump2 = false;
            }
            else if (stopJump2)
            {
                stopJump2 = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model2.jumpDeceleration;
                }
            }

            if (move2.x > 0.01f)
                spriteRenderer2.flipX = false;
            else if (move2.x < -0.01f)
                spriteRenderer2.flipX = true;

            animator2.SetBool("grounded", IsGrounded);
            animator2.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed2);

            targetVelocity = move2 * maxSpeed2;
        }

        public enum JumpState2
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }

    public class PlayerJumped2 : Simulation.Event<PlayerJumped2>
    {
        public PlayerController2 player;

        public override void Execute()
        {
            if (player.jumpAudio2 != null)
                player.audioSource2.PlayOneShot(player.jumpAudio2);
        }
    }

    public class PlayerLanded2 : Simulation.Event<PlayerLanded2>
    {
        public PlayerController2 player;

        public override void Execute()
        {
            // ใส่อะไรเพิ่มก็ได้ตรงนี้
        }
    }

    public class PlayerStopJump2 : Simulation.Event<PlayerStopJump2>
    {
        public PlayerController2 player;

        public override void Execute()
        {
            if (player.velocity.y > 0)
            {
                // ✅ ใช้ property ที่เราเพิ่มเข้าถึง model2 อย่างปลอดภัย
                player.velocity.y = player.velocity.y * player.Model2.jumpDeceleration;
            }
        }
    }
}

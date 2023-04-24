using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;

    public Transform player;
    private Health playerHealth;
    bool playerBlock;

    [Header("Boss Enemy Parameters")]
    public Health bossHealth;
    bool fetchedDeadBool;

    private void Update()
    {
        fetchedDeadBool = bossHealth.GetComponent<Health>().dead;

        Death();
    }

    public bool Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
            if (colInfo.gameObject.CompareTag("Player"))
            {
                playerHealth = colInfo.transform.GetComponent<Health>();
                //playerHurt = true;
            }
            //playerHurt = false;
        }

        return colInfo != null;
    }

	public bool EnragedAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            if (colInfo.gameObject.CompareTag("Player"))
            {
                playerHealth = colInfo.transform.GetComponent<Health>();
                //playerHurt = true;
            }
            //playerHurt = false;
        }

        return colInfo != null;
    }

    private void DamagePlayer()
    {
        if (Attack())
        {
            if (playerBlock)
            {
                playerHealth.TakeDamage(0);
            }
            else
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}

    void Death()
    {
        if (fetchedDeadBool)
        {
            this.enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(Vanish());
        }
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(5);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}

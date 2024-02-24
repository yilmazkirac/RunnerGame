using UnityEngine;
using System.Collections;
public class CharacterInteraction : MonoBehaviour
{
    [Header("CHARACTERS----------------")]
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private CharacterAnimator _characterAnimator;
    [Header("OTHER----------------")]
    [SerializeField] private Material _material;
    private bool isCreateWay;
    private void Start()
    {
        _material.color = Color.white;
    }
    private void IsCreateWay()
    {
        isCreateWay = false;
    }
    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.CoinSound.Play();
            other.gameObject.SetActive(false);
            _characterStats.Point += 25;
        }

        if (other.gameObject.CompareTag("EndWay"))
        {
            if (!isCreateWay)
            {
                int random = Random.Range(0, WayManager.Instance.WayPrefab.Length);
                while (WayManager.Instance.WayPrefab[random].activeInHierarchy)
                {
                    random = Random.Range(0, WayManager.Instance.WayPrefab.Length);
                }
                WayManager.Instance.WayPrefab[random].transform.position = new Vector3(0, 0, other.transform.gameObject.GetComponent<EndWay>().Way.gameObject.transform.position.z + 300);
                WayManager.Instance.WayPrefab[random].SetActive(true);

                other.GetComponent<EndWay>().Invoke("Reset", 0.5f);
                isCreateWay = true;
                Invoke("IsCreateWay", 2f);
            }           
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (!_characterStats.MayHit)
            {
                _characterStats.MayHit = true;
                if (_characterStats.Health > 1)
                {
                    DealDamage("Hit On The Back");
                }
                else if (_characterStats.Health <= 1)
                {
                    Dead();
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 5), .1f);
                }
            }        
        }
        if (other.gameObject.CompareTag("LeftObs"))
        {
            if (!_characterStats.MayHit)
            {
                _characterStats.MayHit = true;
                if (_characterStats.Health > 1)
                {
                    DealDamage("Hit On Side Of Head");                    
                    GameManager.Instance.Player.GetComponent<CharacterMoveRb>().currentPosition -= 1;                   
                }
                else if (_characterStats.Health <= 1)
                {
                    Dead();
                    GameManager.Instance.Player.GetComponent<CharacterMoveRb>().currentPosition -= 1;                 
                }
            }                     
        }


        if (other.gameObject.CompareTag("RightObs"))
        {
            if (!_characterStats.MayHit)
            {
                _characterStats.MayHit = true;
                if (_characterStats.Health > 1)
                {
                    DealDamage("Hit On Side Of Head 1");                
                    GameManager.Instance.Player.GetComponent<CharacterMoveRb>().currentPosition += 1;                 
                }
                else if (_characterStats.Health <= 1)
                {
                    Dead();
                    GameManager.Instance.Player.GetComponent<CharacterMoveRb>().currentPosition += 1;                 
                }
            }       
        }

        if (other.gameObject.CompareTag("CarStart"))
        {
            other.GetComponentInParent<Car>().IsStart = true;
        }
    }
    private void DealDamage(string name)
    {
        _characterAnimator.CrossFade(name, .1f);
        StartCoroutine(ChangeColor());
        _characterStats.Health -= 1;
        UIManager.Instance.HealtText.text = "Health: " + _characterStats.Health;
    }
    private void Dead()        
    {
        GameManager.Instance.GameSound.Stop();
        GameManager.Instance.FinishSound.Play();
        GameManager.Instance.RunTime = 0;
        _characterStats.Speed = 0;
        _characterAnimator.CrossFade("Falling Back Death", .1f);
        _characterStats.Health -= 1;
        _characterStats.SetPoint();
        _material.color = Color.red;
        StartCoroutine(RestartPanel());
        UIManager.Instance.HealtText.text = "Health: " + _characterStats.Health;
        int point =(int)_characterStats.Point;
        UIManager.Instance.ScoreText2.text = point.ToString();
        UIManager.Instance.BestScoreText.text =PlayerPrefs.GetInt("Point").ToString();
     
    }
    IEnumerator RestartPanel()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.RestartPanel.SetActive(true);
        UIManager.Instance.InGamePanel.SetActive(false);
    }
    IEnumerator ChangeColor()
    {
        _material.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        _material.color = Color.white;
        yield return new WaitForSeconds(0.4f);
        _material.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        _material.color = Color.white;
        yield return new WaitForSeconds(0.4f);
        _material.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        _material.color = Color.white;
        _characterStats.MayHits();
    }
}

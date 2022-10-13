using UnityEngine;

public class ParticulesGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _particule;
    [SerializeField] private GameObject _parentForCleanHierachy;
    #region Header
    [Header("PARAMETERS")]
    [Space(10)]
    #endregion
    [SerializeField] private float _cadenceGeneration = 0.2f;
    [SerializeField] private float _randomCircle = 0.5f;
    [SerializeField] private float _linearDrag = 0.01f;
    #region Header
    [Header("FORCE ADDED ALWAYS TO THE RIGHT")]
    [Space(10)]
    #endregion
    [SerializeField] private float _forceToAdd = 5f;

    float _nextGeneration = 0;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _randomCircle);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _nextGeneration)
        {
            _nextGeneration = Time.time + _cadenceGeneration;
            GenerationParticules();
        }
    }

    void GenerationParticules()
    {
        GameObject particle = Instantiate(_particule, gameObject.transform.position + Random.insideUnitSphere * _randomCircle, gameObject.transform.rotation, _parentForCleanHierachy.transform);
        ApplyForces(particle);
    }

    void ApplyForces(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.drag = _linearDrag;
        rb.AddForce(transform.right * _forceToAdd, ForceMode2D.Impulse);
    }
}

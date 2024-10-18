using UnityEngine;

namespace My2D
{
    //발사체(화살) 발사
    public class ProjecttileLauncher : MonoBehaviour
    {
        #region Variables
        public GameObject projectliePrefab;
        public Transform firePoint;
        #endregion

        public void FireProjectile()
        {
            Debug.Log("화살 발사");
            GameObject projectile = Instantiate(projectliePrefab, firePoint.position,projectliePrefab.transform.rotation);

            //화살의 방향 결정
            Vector3 orifinScale = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(
                orifinScale.x * transform.localScale.x > 0 ? 1: -1,
                orifinScale.y,
                orifinScale.z);
            Destroy(projectile,5f);
        }
    }
}
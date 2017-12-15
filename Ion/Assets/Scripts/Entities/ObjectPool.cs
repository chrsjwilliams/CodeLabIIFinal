using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static Queue<GameObject> bluePool = new Queue<GameObject>(); //Queue for bullets
    public static Queue<GameObject> playerPool = new Queue<GameObject>(); //Queue for enemies

    public static GameObject GetFromPool(Poolable.types type)
    {//Get an object from the pool

        GameObject result;

        if (type == Poolable.types.BLUE)
        { //if type is a BULLET
            if (bluePool.Count > 0)
            { //if we have bullets in the bulletPool
                result = bluePool.Dequeue(); //get a bullet ou of the bulletPool
            }
            else
            {
                result = Instantiate(Resources.Load("Blue")) as GameObject; //create a new bullet, since the pool is empty
            }
        }
        else
        {//if (type == Poolable.types.ENEMY){
            Debug.Log("Deueue the player");
            if (playerPool.Count > 0)
            { //if we have enemies in the enemyPool
                result = playerPool.Dequeue(); //get a enemy ou of the enemyPool
            }
            else
            {
                result = Instantiate(Resources.Load("Player1")) as GameObject; //create a new enemy, since the pool is empty
            }
        }

        result.SetActive(true); //Activate the bullet
        result.GetComponent<Poolable>().Reset(); //Call "Reset" for this objects Poolable component

        return result; //return the result
    }

    public static void AddToPool(GameObject obj)
    { //Add an object to the pool
        obj.SetActive(false); //turn off the object

        Poolable p = obj.GetComponent<Poolable>(); //get this objects Poolable component

        if (p is MovingParticle)
        { //if p is a "PoolableBullet"
            bluePool.Enqueue(obj); //put it in the bullet pool
        }
        else if (p is Particle)
        { //if p is a "PoolableEnemy"
            Debug.Log("Queue the player");
            //playerPool.Enqueue(obj); //put it in the enemy pool
        }
        else
        { //if it's an unsupported type of Poolable
            Debug.Log("You have not implemented a pool for this type of Object"); //Print out an warning, don't add it to a pool
        }
    }
}
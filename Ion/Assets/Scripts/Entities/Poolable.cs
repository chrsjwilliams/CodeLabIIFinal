using UnityEngine;

//Poolable is a class component that should be attached
//to any GameObject you want to Pool
//It is an "abstract" class, which means it can not be
//create instances itself, only it's non-abstract subclasses
//can actually used to create objects
public abstract class Poolable : MonoBehaviour
{

    //Types is an "enum" that describes types of Poolable objects
    public enum types
    {
        PLAYER, BLUE
    }

    void Start()
    {
        Setup(); //Start calls the abstract method "Setup"
    }

    void Update()
    {
        if (RePool())
        { //if the abstract method "Repool" returns true
            ObjectPool.AddToPool(gameObject); //add the gameObject attached to this component to the ObjectPool
        }
    }

    //abstract methods are not implemented in the abstract class,
    //but all non-abstract subclasses *must* override them
    public abstract void Setup(); //Setup a Poolable object

    public abstract bool RePool();//Check to see if the object should be repooled

    public abstract void Reset(); //Reset the object to allow it to be recycled
}
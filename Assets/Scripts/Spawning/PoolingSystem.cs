using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class PoolingSystem : MonoBehaviour {

	[System.Serializable]
	public class PoolingItems
	{
		public GameObject prefab;
		public int amount;
	}

	public static PoolingSystem Instance;

    public bool SpawnedItemsGoToPool = true;


	/// These fields will hold all the different types of assets you wish to pool.
	public List<PoolingItems> poolingItems;
	public List<List<GameObject>> pooledItems;


	/// The default pooling amount for each object type, in case the pooling amount is not mentioned or is 0.
	public int defaultPoolAmount = 10;


	/// Do you want the pool to expand in case more instances of pooled objects are required.
	public bool poolExpand = true;

	void Awake ()
	{
		Instance = this;
	}

	void Start()
	{
		CheckPooledItemsInitialized();
	}

	void CheckPooledItemsInitialized()
    {
		if(pooledItems == null)
        {
			Initialize();
        }
    }

    private void Initialize()
    {
    pooledItems = new List<List<GameObject>>();

		for(int i=0; i<poolingItems.Count; i++)
		{
			pooledItems.Add(new List<GameObject>());

			int poolingAmount;
			if(poolingItems[i].amount > 0) poolingAmount = poolingItems[i].amount;
			else poolingAmount = defaultPoolAmount;

			for(int j=0; j<poolingAmount; j++)
			{
				GameObject newItem = (GameObject) Instantiate(poolingItems[i].prefab);
				newItem.SetActive(false);
				pooledItems[i].Add(newItem);
				newItem.transform.SetParent(transform);
			}
		}
	}

	public static void DisableAPS(GameObject myObject)
	{
		Instance.enabled = false;
	}


    public void AddPoolingItem(GameObject poolingItem, GameObject pooledItem)
    {
        newPoolinItem = new PoolingItems();
        newPoolinItem.prefab = poolingItem;
        newPoolinItem.amount = 1;
        poolingItems.Add(newPoolinItem);
        pooledItems.Add(new List<GameObject>());
        pooledItems[pooledItems.Count-1].Add(pooledItem);
    }

    public void AddPoolingItemWithoutInstance(GameObject poolingItem)
    {
        newPoolinItem = new PoolingItems();
        newPoolinItem.prefab = poolingItem;
        newPoolinItem.amount = 0;
        poolingItems.Add(newPoolinItem);
        pooledItems.Add(new List<GameObject>());
    }

    public GameObject InstantiateAPS (string itemType, System.Action<GameObject> actionAfterSpawning)
	{
		GameObject newObject = GetPooledItem(itemType);
		if(newObject != null) {
			newObject.SetActive(true);
            if (actionAfterSpawning != null)
            {
                actionAfterSpawning.Invoke(newObject);
            }
            return newObject;
        }
        //Debug.Log("Warning: Pool is out of objects.\nTry enabling 'Pool Expand' option.");
        return null;
	}

    PoolingItems newPoolinItem;


    public GameObject InstantiateAPS (string itemType, Vector3 itemPosition, Quaternion itemRotation, System.Action<GameObject> actionAfterSpawning)
	{
		GameObject newObject = GetPooledItem(itemType);
		if(newObject != null) {
			newObject.transform.position = itemPosition;
            if(itemRotation != Quaternion.identity)
	            newObject.transform.rotation = itemRotation;
			newObject.SetActive(true);
            if(actionAfterSpawning != null)
            {
                actionAfterSpawning.Invoke(newObject);
            }
			return newObject;
        }
		return null;
	}

	public GameObject InstantiateAPS (string itemType, Vector3 itemPosition, Quaternion itemRotation, GameObject myParent, System.Action<GameObject> actionAfterSpawning)
	{
		CheckPooledItemsInitialized();
		GameObject newObject = GetPooledItem(itemType);
		if(newObject != null) {
			newObject.transform.position = itemPosition;
            if (itemRotation != Quaternion.identity)
                newObject.transform.rotation = itemRotation;
			newObject.transform.SetParent(myParent.transform);
			newObject.SetActive(true);
            if (actionAfterSpawning != null)
            {
                actionAfterSpawning.Invoke(newObject);
            }
            return newObject;
		}
        return null;
	}




    public static void PlayEffect(GameObject particleEffect, int particlesAmount)
	{
		if(particleEffect.GetComponent<ParticleSystem>())
		{
			particleEffect.GetComponent<ParticleSystem>().Emit(particlesAmount);
		}
	}

	public static void PlaySound(GameObject soundSource)
	{
		if(soundSource.GetComponent<AudioSource>())
		{
			soundSource.GetComponent<AudioSource>().PlayOneShot(soundSource.GetComponent<AudioSource>().clip);
		}
	}

	public GameObject GetPooledItem(string itemType)
	{
		for(int i=0; i<poolingItems.Count; i++)
		{
			if(poolingItems[i].prefab.name == itemType)
			{
				if(pooledItems != null && pooledItems.Count > i)
					for(int j=0; j<pooledItems[i].Count; j++)
					{
						if(!pooledItems[i][j].activeInHierarchy)
						{
							return pooledItems[i][j];
						}
					}

				if(poolExpand)
				{
					GameObject newItem = (GameObject) Instantiate(poolingItems[i].prefab, transform, true);
					newItem.SetActive(false);
					pooledItems?[i].Add(newItem);
					return newItem;
				}

				break;
			}
		}

		return null;
	}

}

public static class PoolingSystemExtensions
{
	public static void DisableAPS(this GameObject myobject)
	{
		PoolingSystem.DisableAPS(myobject);
	}

	public static void PlayEffect(this GameObject particleEffect, int particlesAmount)
	{
		PoolingSystem.PlayEffect(particleEffect, particlesAmount);
	}

	public static void PlaySound(this GameObject soundSource)
	{
		PoolingSystem.PlaySound(soundSource);
	}
}
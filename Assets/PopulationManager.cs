using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager: MonoBehaviour {

	public GameObject personPrefab;
	public int populationSize = 10;
	List < GameObject > population = new List < GameObject > ();
	public static float elapsedTime = 0f;

	public int trialTime = 10;
	public int generation = 1;
	// Use this for initialization
	void Start()
    {
        InitiatePopulation();
		
		InvokeRepeating("RandomSelection",1f, (float)trialTime/populationSize); 
    }

	GUIStyle guiStyle = new GUIStyle();
	void OnGUI()
	{
		guiStyle.fontSize = 30;
		guiStyle.normal.textColor = Color.white;
		GUI.Label(new Rect(10,10,100,20), "Generation : "+generation, guiStyle);
		GUI.Label(new Rect(10,65,100,20), "Elapsed Time : "+(int)elapsedTime, guiStyle);
	}

    private void InitiatePopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 4f;
			pos.x += Random.Range(2f,5f);
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
			go.name = "Person "+i;
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
			go.GetComponent<DNA>().scale = Random.Range(0.1f, 0.4f);

            population.Add(go);
        }
    }

	void BreedNewPopulation(){
		// sorted list by time to death
		List<GameObject> sortedList = population.OrderBy( o => o.GetComponent<DNA>().timeToDie).ToList();
		population.Clear();

		for(int i= (int) (sortedList.Count / 2)-1; i < sortedList.Count - 1; i++ ){
			// breeding new population based on previous population 
			population.Add(Breed(sortedList[i], sortedList[i+1]));
			population.Add(Breed(sortedList[i+1], sortedList[i]));
		}

		// destroy all parents and previous population 
		foreach(GameObject g in sortedList){ Destroy(g); }
		
		// increase the generation 
		generation++;
	}

	GameObject Breed(GameObject parent1, GameObject parent2){
		Vector2 pos = Random.insideUnitCircle * 5f;
		// inherited child form parent
		GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
		
		DNA dna1 = parent1.GetComponent<DNA>();
		DNA dna2 = parent2.GetComponent<DNA>();

		// swap parent data randomly not average 
		// this is what makes genetics algorithm works 
		// 50% chance you can inehrit form dna1 or dna2 
		
		// add small mutation 
		if(Random.Range(0,1000) > 10){
			offspring.GetComponent<DNA>().r = Random.Range(0,10) < 5 ? dna1.r : dna2.r;
			offspring.GetComponent<DNA>().g = Random.Range(0,10) < 5 ? dna1.g : dna2.g;
			offspring.GetComponent<DNA>().b = Random.Range(0,10) < 5 ? dna1.b : dna2.b;
			offspring.GetComponent<DNA>().scale = Random.Range(0,10) < 5 ? dna1.scale : dna2.scale;
		}
		else{
			offspring.GetComponent<DNA>().r = Random.Range(0f,1f);
			offspring.GetComponent<DNA>().g = Random.Range(0f,1f);
			offspring.GetComponent<DNA>().b = Random.Range(0f,1f);
			offspring.GetComponent<DNA>().scale = Random.Range(0.1f, 0.4f);
		}

		return offspring;

	}

    // Update is called once per frame
    void Update() {
		elapsedTime += Time.deltaTime;
		if(elapsedTime > trialTime){
			elapsedTime = 0;
			BreedNewPopulation();
		}
	}
	
	public void RandomSelection(){ 

   // int rnd = UniqueRandomInt(0, population.Count); 
    int rnd = Random.Range(0,population.Count);

    GameObject c = population[rnd]; 
    c.GetComponent<DNA>().RemovePerson(); 
     
    Debug.Log("Random Seed "+rnd); 
     
	} 
 
 
  /// <summary> 
  /// Returns all numbers, between min and max inclusive, once in a random sequence. 
  /// </summary> 
   List<int> usedValues = new List<int>(); 
   public int UniqueRandomInt(int min, int max){ 
      
   int val = Random.Range(min, max); 
     while(usedValues.Contains(val)){ 
         val = Random.Range(min, max); 
     } 
     return val; 
   } 


}
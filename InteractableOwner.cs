using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//WIP two handed interaction
public class InteractableOwner : MonoBehaviour
{
    public Interactable[] gripPoints = new Interactable[0];

    public List<Interactable> activePoints = new List<Interactable>();
    Interactable leftGrasped => activePoints.Where(x => x.interactor = Interactor.left).FirstOrDefault();
    Interactable rightGrasped => activePoints.Where(x => x.interactor = Interactor.right).FirstOrDefault();

    private void Update()
    {
        int activeCount = activePoints.Count();
        if (activeCount == 0)
        {

        }
        else if (activeCount == 1)
        {

        }
        else if (activeCount == 2)
        {

        }
    }

    public void OnStateChange (Interactable i)
    {
        if (i.InHand && !activePoints.Contains(i))
        {
            activePoints.Add(i);
        } 
        else if (!i.InHand && activePoints.Contains(i))
        {
            activePoints.Remove(i);
        }
    }
}

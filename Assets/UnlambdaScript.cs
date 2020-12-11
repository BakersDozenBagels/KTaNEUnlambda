using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO:
 * Make flashes work
 * Make buttons input
 * Make hard mode activate work
 * Fix manual
 */

public class UnlambdaScript : MonoBehaviour {
    public Transform HardModeButtons;
    public KMBombModule Module;
    public KMBombInfo Info;

    private string program = "";
    private int strikes = 0;
    private bool hardMode = false;

	// Use this for initialization
	void Start () {
        HardModeButtons.localScale = new Vector3(0f, 0f, 0f);
        Module.OnActivate += delegate () { Activate(); };
	}
	
	// Update is called once per frame
	void Update () {
		if (Info.GetStrikes() > strikes)
        {
            for (int i = 0; i < Info.GetStrikes() - strikes; i++)
            {
                if (!hardMode)
                {
                    program = ULInterface.Generate(5, true) + program;
                }
                else
                {
                    program = ULInterface.GenerateCruel(5, true) + program;
                }
            }
        }
	}

    private void Activate()
    {
        program = ULInterface.Generate(10, false);
    }

    private IEnumerator ActivateHardMode()
    {
        for(int i = 0; i < 100; i++)
        {
            HardModeButtons.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        hardMode = true;
        HardModeButtons.localScale = new Vector3(1f, 1f, 1f);
        program = ULInterface.GenerateCruel(10, false);
        for (int i = 0; i < strikes; i++)
        {
            program = ULInterface.GenerateCruel(5, true) + program;
        }
    }
}

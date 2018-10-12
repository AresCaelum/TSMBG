using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugCatcherUnitTests : MonoBehaviour
{
    void Start()
    {
        //Enables Debug.Log entries in the Bug Catcher
        BugCatcher.Instance.AllowDebugLog = true;
        Debug.Log("Test 1");//This should show in both console and Bug Catcher.
        Debug.Log("Test_2");//This should show in both console and Bug Catcher.
        Debug.Log("Test Three");//This should show in both console and Bug Catcher.

        //Disables Debug.Log entries in the Bug Catcher
        BugCatcher.Instance.AllowDebugLog = false;
        Debug.Log("Test 4");//This should NOT show in Bug Catcher but should show in console.
        Debug.Log("Test_5");//This should NOT show in Bug Catcher but should show in console.
        Debug.Log("Test Six");//This should NOT show in Bug Catcher but should show in console.

        //Toggles the Bug Catcher window on and off (in this case, On)
        BugCatcher.Instance.ToggleDisplay();
        BugCatcher.Instance.AddEntry("Manual Test 1", MASTER_DEBUGGER_MESSAGE_TYPE.ASSERT);//This should show in Bug Catcher but not in console.
        BugCatcher.Instance.AddEntry("Manual Test_2", MASTER_DEBUGGER_MESSAGE_TYPE.ERROR);//This should show in Bug Catcher but not in console.
        BugCatcher.Instance.AddEntry("Manual Test Three", MASTER_DEBUGGER_MESSAGE_TYPE.FAILURE);//This should show in Bug Catcher but not in console.

        //This adds huge entries to test the size of the entries.
        BugCatcher.Instance.AddEntry("1) After Trinidad became the IBF's world Welterweight champion in 1993, Chade continued on trying to make him well known across the world; Trinidad defended his title once in Monterrey, Mexico, in 1994. The relationship between Chade and the Trinidads became so bad that, in 1995, a court dissolved the contract tying Chade and the Trinidads. Perhaps fed up with boxing, Chade turned to basketball, buying the BSN's Arecibo Captains in 2003. The Captains had not won a Puerto Rican national championship tournament since 1959, and when Chade and other team personnel promised the people of Arecibo, Puerto Rico that they would be champions again soon, they were met with skeptics, among Puerto Rican basketball fans and sports writers. After signing Larry Ayuso, Edgar Padilla, Mario Butler, Keenon Jourdan, Dickey Simpkins, and Sharif Fajardo, among others, the Captains became champions for the second time in their team's history, when they defeated the Bayamón Cowboys in four games at the 2005 BSN finals.");
        BugCatcher.Instance.AddEntry("2) After Trinidad became the IBF's world Welterweight champion in 1993, Chade continued on trying to make him well known across the world; Trinidad defended his title once in Monterrey, Mexico, in 1994. The relationship between Chade and the Trinidads became so bad that, in 1995, a court dissolved the contract tying Chade and the Trinidads. Perhaps fed up with boxing, Chade turned to basketball, buying the BSN's Arecibo Captains in 2003. The Captains had not won a Puerto Rican national championship tournament since 1959, and when Chade and other team personnel promised the people of Arecibo, Puerto Rico that they would be champions again soon, they were met with skeptics, among Puerto Rican basketball fans and sports writers. After signing Larry Ayuso, Edgar Padilla, Mario Butler, Keenon Jourdan, Dickey Simpkins, and Sharif Fajardo, among others, the Captains became champions for the second time in their team's history, when they defeated the Bayamón Cowboys in four games at the 2005 BSN finals.");
        BugCatcher.Instance.AddEntry("3) After Trinidad became the IBF's world Welterweight champion in 1993, Chade continued on trying to make him well known across the world; Trinidad defended his title once in Monterrey, Mexico, in 1994. The relationship between Chade and the Trinidads became so bad that, in 1995, a court dissolved the contract tying Chade and the Trinidads. Perhaps fed up with boxing, Chade turned to basketball, buying the BSN's Arecibo Captains in 2003. The Captains had not won a Puerto Rican national championship tournament since 1959, and when Chade and other team personnel promised the people of Arecibo, Puerto Rico that they would be champions again soon, they were met with skeptics, among Puerto Rican basketball fans and sports writers. After signing Larry Ayuso, Edgar Padilla, Mario Butler, Keenon Jourdan, Dickey Simpkins, and Sharif Fajardo, among others, the Captains became champions for the second time in their team's history, when they defeated the Bayamón Cowboys in four games at the 2005 BSN finals.");
        BugCatcher.Instance.AddEntry("4) After Trinidad became the IBF's world Welterweight champion in 1993, Chade continued on trying to make him well known across the world; Trinidad defended his title once in Monterrey, Mexico, in 1994. The relationship between Chade and the Trinidads became so bad that, in 1995, a court dissolved the contract tying Chade and the Trinidads. Perhaps fed up with boxing, Chade turned to basketball, buying the BSN's Arecibo Captains in 2003. The Captains had not won a Puerto Rican national championship tournament since 1959, and when Chade and other team personnel promised the people of Arecibo, Puerto Rico that they would be champions again soon, they were met with skeptics, among Puerto Rican basketball fans and sports writers. After signing Larry Ayuso, Edgar Padilla, Mario Butler, Keenon Jourdan, Dickey Simpkins, and Sharif Fajardo, among others, the Captains became champions for the second time in their team's history, when they defeated the Bayamón Cowboys in four games at the 2005 BSN finals.");
        BugCatcher.Instance.AddEntry("5) After Trinidad became the IBF's world Welterweight champion in 1993, Chade continued on trying to make him well known across the world; Trinidad defended his title once in Monterrey, Mexico, in 1994. The relationship between Chade and the Trinidads became so bad that, in 1995, a court dissolved the contract tying Chade and the Trinidads. Perhaps fed up with boxing, Chade turned to basketball, buying the BSN's Arecibo Captains in 2003. The Captains had not won a Puerto Rican national championship tournament since 1959, and when Chade and other team personnel promised the people of Arecibo, Puerto Rico that they would be champions again soon, they were met with skeptics, among Puerto Rican basketball fans and sports writers. After signing Larry Ayuso, Edgar Padilla, Mario Butler, Keenon Jourdan, Dickey Simpkins, and Sharif Fajardo, among others, the Captains became champions for the second time in their team's history, when they defeated the Bayamón Cowboys in four games at the 2005 BSN finals.");
        BugCatcher.Instance.AddEntry("6) After Trinidad became the IBF's world Welterweight champion in 1993, Chade continued on trying to make him well known across the world; Trinidad defended his title once in Monterrey, Mexico, in 1994. The relationship between Chade and the Trinidads became so bad that, in 1995, a court dissolved the contract tying Chade and the Trinidads. Perhaps fed up with boxing, Chade turned to basketball, buying the BSN's Arecibo Captains in 2003. The Captains had not won a Puerto Rican national championship tournament since 1959, and when Chade and other team personnel promised the people of Arecibo, Puerto Rico that they would be champions again soon, they were met with skeptics, among Puerto Rican basketball fans and sports writers. After signing Larry Ayuso, Edgar Padilla, Mario Butler, Keenon Jourdan, Dickey Simpkins, and Sharif Fajardo, among others, the Captains became champions for the second time in their team's history, when they defeated the Bayamón Cowboys in four games at the 2005 BSN finals.");

        //This should add an entry to the Bug Catcher for a null error.
        GameObject nullTest = null;
        if(nullTest.name == "")
        {
            Debug.Log("This should not display");
        }
    }
}
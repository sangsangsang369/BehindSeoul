using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;

public class DistanceBetweenTwoPoints : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI distanceText;
    [SerializeField]
    GameObject startBtn;
    //public InputField ViaUnityResult;
    SpawnOnMap spawnOnMap;
    ImmediatePositionWithLocationProvider immelocationProvider;
    double distance;

    private void Awake() 
    {
        spawnOnMap = FindObjectOfType<SpawnOnMap>(); 
        immelocationProvider = FindObjectOfType<ImmediatePositionWithLocationProvider>();   
    }

    public void Calculate()
    {
        Vector2d playerLoc = immelocationProvider.LocationProvider.CurrentLocation.LatitudeLongitude;
        Vector2d spot1Loc = spawnOnMap._locations[0];

        Vector2d fromMeters = Conversions.LatLonToMeters(playerLoc);
        Vector2d toMeters = Conversions.LatLonToMeters(spot1Loc);
        distance = (spot1Loc - playerLoc).magnitude * 100;
        distanceText.text = string.Format("{0:0.#}", distance) + "km";
        
        /*Vector3 fromUnity = Map.GeoToWorldPosition(playerLoc, false);
        Vector3 toUnity = Map.GeoToWorldPosition(spot1Loc, false);
        float distanceUnity = new Vector2( toUnity.x - fromUnity.x, toUnity.z - fromUnity.z).magnitude / Map.WorldRelativeScale;
        ResultText.text = distanceUnity.ToString();*/
    }

    public void IsDistanceUnder500m()
    {
        Calculate();
        if(distance < 0.5f)
        {
            startBtn.SetActive(true);
        }
    }
}

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    SaveSystem loadSystem = new SaveSystem();
    protected SaveSystem saveGame;
    protected float JumpHeight = 10f;
    protected float Speed = 5f;
    protected float SprintSpeed = 9f;
    protected float DetectionSpeed = 3f;
    protected float Sensitivity = 0f;
    protected int permCash = 0;
    protected int levelCash = 0;
    protected int DetectionLevel = 0;
    protected int maxDetection = 75;
    protected bool unlockedLevel2 = false;
    protected bool cameraDisabled = false;
    protected int cashMultiplyer = 1;
    protected int cameraDisableCount = 0;
    protected bool unlockedBonus;
    
    [NonSerialized]
    public bool isDetected;

    private void Start()
    {
        saveGame = JsonUtility.FromJson<SaveSystem>(loadSystem.LoadFile());

        JumpHeight = saveGame.getJumpHeight();
        Speed = saveGame.getSpeed();
        SprintSpeed = saveGame.getSprintSpeed();
        DetectionSpeed = saveGame.getDetectionSpeed();
        Sensitivity = saveGame.getSensitivity();
        permCash = saveGame.getPermCash();
        levelCash = 0;
        maxDetection = saveGame.getMaxDetection();
        unlockedLevel2 = saveGame.getUnlockedLevel2();
        cashMultiplyer = saveGame.getCashMultiplyer();
        cameraDisableCount = saveGame.getCameraDisableCount();
        unlockedBonus = saveGame.getUnlockedBonus();

        print(JsonUtility.ToJson(saveGame));

        StartCoroutine(IncreaseDetectionLevel(0.2));
        StartCoroutine(DecreaseDetectionLevel(0.3));
        
        if(gameObject.GetComponent<PlayerMovement>() != null)
        {
            gameObject.GetComponent<PlayerMovement>().doStartStuff();
        }

    }
    public void SavePlayerStuff() {
        loadSystem.SaveFile(saveGame);
    }
    public float getJumpHeight() { return JumpHeight; }
    public float getSpeed() { return Speed; }
    public float getSprintSpeed() { return SprintSpeed; }
    public float getDetectionSpeed() { return DetectionSpeed; }
    public float getSensitivity() { return Sensitivity; }
    public int getPermCash() { return permCash; }
    public int getLevelCash() { return levelCash; }
    public int getDetectionLevel() { return DetectionLevel; }
    public int getMaxDetection() { return maxDetection; }
    public bool getUnlockedLevel2() { return unlockedLevel2; }
    public int getCashMultiplyer() { return cashMultiplyer; }
    public int getCameraDisableCount() { return cameraDisableCount; }
    public bool getUnlockedBonus() { return unlockedBonus; }

    public void disableCameras() {
        cameraDisabled = true;
    }
    public void enableCameras() {
        cameraDisabled = false;
    }

    public void setJumpHeight(float newJumpHeight) {
        JumpHeight = newJumpHeight;
        saveGame.setJumpHeight(newJumpHeight);
    }
    public void setSpeed(float newSpeed) {
        Speed = newSpeed;
        saveGame.setJumpHeight(newSpeed);
    }
    public void setSprintSpeed(float newSprintSpeed) {
        SprintSpeed = newSprintSpeed;
        saveGame.setSprintSpeed(newSprintSpeed);
    }
    public void setDetectionSpeed(float newDetectionRadius) {
        DetectionSpeed = newDetectionRadius;
        saveGame.setDetectionSpeed(newDetectionRadius);
    }
    public void setSensitivity(float newSensitivity) {
        Sensitivity = newSensitivity;
        saveGame.setSensitivity(newSensitivity);
    }
    public void setPermCash(int newCash) {
        permCash = newCash;
        saveGame.setPermCash(newCash);
    }
    public void addPermCash(int addCash) {
        permCash += addCash;
        saveGame.addCash(addCash);
    }
    public void setLevelCash(int newLevelCash) {
        levelCash = newLevelCash;
    }
    public void addLevelCash(int addCash) {
        levelCash += addCash;
    }
    public void setDetectionLevel(int newDetectionLevel) {
        DetectionLevel = newDetectionLevel;
    }
    public void addDetectionLevel(int addLevel) {
        DetectionLevel += addLevel;
    }
    public void setMaxDetection(int newMax) {
        maxDetection = newMax;
        saveGame.setMaxDetection(newMax);
    }
    public void setUnlockedLevel2(bool newLevel2) {
        unlockedLevel2 = newLevel2;
        saveGame.setUnlockedLevel2(newLevel2);
    }
    public void setCashMultiplyer(int newCashMultiplyer)
    {
        cashMultiplyer = newCashMultiplyer;
        saveGame.setCashMultiplyer(newCashMultiplyer);
    }
    public void setCameraDisableCount(int newCameraDisableCount)
    {
        cameraDisableCount = newCameraDisableCount;
        saveGame.setCameraDisableCount(newCameraDisableCount);
    }
    public void setUnlockedBonus(bool newUnlockedBonus)
    {
        unlockedBonus = newUnlockedBonus;
        saveGame.setUnlockedBonus(newUnlockedBonus);
    }

    IEnumerator IncreaseDetectionLevel(double delay)
    {
        while (true)
        {
            yield return new WaitForSeconds((float)delay);
            
            if(isDetected && DetectionLevel < maxDetection && !cameraDisabled)
            {
                addDetectionLevel((int)getDetectionSpeed());
            }

        }
    }
    IEnumerator DecreaseDetectionLevel(double delay)
    {
        while (true)
        {
            yield return new WaitForSeconds((float)delay);
            
            if (!isDetected && DetectionLevel > 0)
            {
                addDetectionLevel(-1);
            }
        }
    }
}

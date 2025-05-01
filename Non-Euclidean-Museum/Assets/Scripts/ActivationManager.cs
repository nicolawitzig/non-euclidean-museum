using UnityEngine;

using System.Collections.Generic;
using System.Linq;

public class ActivationManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Player player;
    List<GameObject> rooms = new List<GameObject>();
    GameObject[] allObjects;
    GameObject previousRoom;
    void Awake()
    {
        allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("Room"))
            {
                rooms.Add(obj);
            }
        }
        rooms.Sort((a, b) =>
        {
            int aNum = ExtractRoomNumber(a.name);
            int bNum = ExtractRoomNumber(b.name);
            return aNum.CompareTo(bNum);
        });
        previousRoom = rooms[0];
        Debug.Log($"Found {rooms.Count} room(s).");
    }
    int ExtractRoomNumber(string name)
    {
        string digits = new string(name.Where(char.IsDigit).ToArray());
        return int.TryParse(digits, out int number) ? number : 0;
    }
    private void Start()
    {
        foreach (GameObject room in rooms)
        {
            room.SetActive(false);
        }
        rooms[0].SetActive(true);
        rooms[1].SetActive(true);
        rooms[rooms.Count - 1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject currentRoom = CurrentRoom();
        if (currentRoom == null) return;

        if (ExtractRoomNumber(previousRoom.name) != ExtractRoomNumber(currentRoom.name))
        {
            Debug.Log($"room changed to {currentRoom.name}");
            previousRoom = currentRoom;

            // Deactivate all rooms
            foreach (GameObject room in rooms)
            {
                room.SetActive(false);
            }

            // Activate current room and its neighbors (with wraparound)
            int count = rooms.Count;
            int currentIndex = rooms.FindIndex(r => r == currentRoom);
            int prevIndex = (currentIndex - 1 + count) % count;
            int nextIndex = (currentIndex + 1) % count;

            rooms[prevIndex].SetActive(true);
            rooms[currentIndex].SetActive(true);
            rooms[nextIndex].SetActive(true);

            foreach (Transform child in rooms[prevIndex].transform)
            {
                if (child.name.StartsWith("StencilPortal"))
                {
                    child.gameObject.SetActive(true); // Explicitly re-enable
                }
            }
            foreach (Transform child in rooms[nextIndex].transform)
            {
                if (child.name.StartsWith("StencilPortal"))
                {
                    child.gameObject.SetActive(true); // Explicitly re-enable
                }
            }

            foreach (Transform child in currentRoom.transform)
            {
                if (child.name.StartsWith("StencilPortal"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    public GameObject CurrentRoom()
    {
        float minDistance = float.MaxValue;
        GameObject closestRoom = null;

        foreach (GameObject room in rooms)
        {
            if (!room.activeInHierarchy) continue;
            float distance = Vector3.Distance(player.transform.position, room.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestRoom = room;
            }
        }

        return closestRoom; // Player is not in any known room
    }



}

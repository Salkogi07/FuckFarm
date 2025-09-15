using UnityEngine;

public class Plot : MonoBehaviour
{
    public bool isPlanted = false;
    private Plant currentPlant;

    // ������ �ɴ� �Լ�
    public bool PlantSeed(GameObject plantPrefab, Tile parentTile)
    {
        if (isPlanted)
        {
            Debug.Log("�̹� �۹��� �ɾ��� �ֽ��ϴ�.");
            return false;
        }

        isPlanted = true;
        GameObject plantObject = Instantiate(plantPrefab, transform.position + Vector3.up * 0.1f, Quaternion.identity);
        plantObject.transform.SetParent(transform);
        currentPlant = plantObject.GetComponent<Plant>();
        currentPlant.Initialize(parentTile);

        Debug.Log("������ �ɾ����ϴ�. ���� �ּ���.");
        return true;
    }

    // ���� �ִ� �Լ�
    public void Water()
    {
        if (isPlanted && currentPlant != null && currentPlant.currentState == PlantState.Seeded)
        {
            currentPlant.StartGrowing();
            Debug.Log("���� �־� �Ĺ��� �ڶ�� �����մϴ�.");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Quest System/Quest Actions/Start cutscene")]
public class QuestActionStartCutscene : QuestAction
{
    public SceneIndexer.SceneType sceneLoad;
    public SceneIndexer.SceneType sceneUnload;

    public override void Do()
    {
        SceneManager.LoadSceneAsync((int)sceneLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync((int)sceneUnload);
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.TutorialScene2, LoadSceneMode.Additive).completed += delegate
        {
            GameManager.Instance.currentLevelSceneIndex = (int)SceneIndexer.SceneType.TutorialScene2;
            GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveAllInteractions();
            foreach (Inventory.InventorySlot slot in GameManager.Instance.player.GetComponent<Player>().inventory.inventorySlots)
            {
                if (slot.IsEmpty()) continue;
                if (slot.GetItem().itemData == ItemAssets.Instance.itemsData.Find(x => x.name == "Mar3K"))
                {
                    slot.GetItem().itemData.itemUseAction.Do(slot.GetItem());
                }
            }
        };
        //Start the cutscene
        //LevelLoader.Instance.LoadSceneTransition(sceneLoad, sceneUnload);
    }
}

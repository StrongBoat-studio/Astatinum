using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item use action - Mar3K")]
public class ItemUseMar3K : ItemUseAction
{
    public Transform mar3kPrefab;

    public override void Do(Item item)
    {
        if(GameManager.Instance.player.GetComponent<Player>().inventory.RemoveItem(item))
        {
            GameManager.Instance.player.GetComponent<Player>().journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes.Find(x => x.noteID == 2) });
            Quest q = GameManager.Instance.player.GetComponent<Player>().questSystem.activeQuests.Find(x => x.questData.questID == 201);
            GameManager.Instance.player.GetComponent<Player>().questSystem.AddQuest(QuestAssets.Instance.CreateQuest(QuestData.QuestType.Talk, QuestAssets.Instance.questTalk.Find(x => x.questID == 403).questID));
            GameManager.Instance.player.GetComponent<Player>().questSystem.RemoveQuest(q);
            Instantiate(mar3kPrefab, GameManager.Instance.player.position, Quaternion.identity);
            AudioManager.Instance.PlayGameMar3KStartup();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Journal : MonoBehaviour
{
    private Journal journal;
    [SerializeField] private Transform PostContainer;
    [SerializeField] private Transform Post;

    private void Awake()
    {
        //PostContainer = transform.Find("PostContainer");
        //Post = transform.Find("Post");
    }

    public void SetJournal (Journal journal)
    {
        this.journal = journal;
        RefreshJournalPost();
    }

    private void RefreshJournalPost()
    {
        int x = 0;
        float y = PostContainer.GetComponent<RectTransform>().rect.yMin;
        foreach (Post post in journal.GetPostList())
        {
            RectTransform postSlotRectTransform = Instantiate(Post, PostContainer).GetComponent<RectTransform>();
            postSlotRectTransform.gameObject.SetActive(true);
            postSlotRectTransform.localPosition = new Vector2(x, y);
            y+=Post.GetComponent<RectTransform>().rect.height;
        }
    }
}

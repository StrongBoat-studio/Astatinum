using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal
{
    private List<Post> postList;

    public Journal()
    {
        postList = new List<Post>();

        AddPost(new Post { postType = Post.PostType.History });
        AddPost(new Post { postType = Post.PostType.Life });
        Debug.Log(postList.Count);
    }

    public void AddPost(Post post)
    {
        postList.Add(post);
    }

    public List<Post> GetPostList()
    {
        return postList;
    }

}

using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

[ExecuteInEditMode]
public class BouncyBallInitializer : MonoBehaviour
{
    [SerializeField] private SpriteSkin _spriteSkin;

    [UsedImplicitly]
    public void InitBones()
    {
        Transform[] bones = _spriteSkin.boneTransforms;

        for (int i = 0; i <  bones.Length; i++)
        {
            Transform bone = bones[i];
            bone.AddComponent<CircleCollider2D>();
            bone.AddComponent<Rigidbody2D>();
        }

        for (int i = 0; i < bones.Length; i++)
        {
            AddJoints(i, bones);
            
        }
    }

    private void AddJoints(int i, Transform[] bones)
    {
        Transform bone = bones[i];
        if (bone.GetComponents<SpringJoint2D>().Length == 3)
        {
            return;
        }
        
        SpringJoint2D joint = bone.AddComponent<SpringJoint2D>();
        int boneLeftIndex = i == 0 ? bones.Length - 1 : i - 1;
        joint.connectedBody = bones[boneLeftIndex].GetComponent<Rigidbody2D>();
            
        joint = bone.AddComponent<SpringJoint2D>();
        int boneRightIndex = i == bones.Length - 1 ? 0 : i + 1;
        joint.connectedBody = bones[boneRightIndex].GetComponent<Rigidbody2D>();
            
        joint = bone.AddComponent<SpringJoint2D>();

        int boneOppositeIndex = (i + bones.Length / 2) % bones.Length;
        joint.connectedBody = bones[boneOppositeIndex].GetComponent<Rigidbody2D>();    }

    void Start()
    {
        InitBones();
    }
}

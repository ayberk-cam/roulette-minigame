[System.Serializable]
public abstract class SaveObject
{
    public virtual void Serialize() { }

    public virtual void Deserialize() { }

    public virtual void SetDefault() { }
}
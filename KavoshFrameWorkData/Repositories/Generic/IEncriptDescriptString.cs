namespace KavoshFrameWorkData.Repositories.Generic
{
    public interface IEncriptDescriptString
    {
        string EncryptString(string clearText);
        string DecryptString(string cipherText);
    }
}

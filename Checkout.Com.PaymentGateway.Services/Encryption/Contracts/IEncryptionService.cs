namespace Checkout.Com.PaymentGateway.Services.Encryption.Contracts
{
    public interface IEncryptionService
    {
        byte[] Encrypt(string original);
        string Decrypt(byte[] encrypted);
    }
}

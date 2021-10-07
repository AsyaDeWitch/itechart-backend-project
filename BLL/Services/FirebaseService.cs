using BLL.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly string _apiKey = "AIzaSyBzLI5ruZ2xbFKxdGzGYMkY2-RMZFVhOvw";
        private readonly string _bucket = "lab-web-app-299ef.appspot.com";
        private readonly string _authEmail = "itechartlabtester@gmail.com";
        private readonly string _authPassword = "AV9Laaqpq9N5PZH1HemL";

        public async Task<string> UploadBackgroundImageAsync(IFormFile backgroundImageFile)
        {
            if (backgroundImageFile.Length > 0)
            {
                return await UploadFileAsync(backgroundImageFile, "backgrounds");
            }
            return null;
        }

        public async Task<string> UploadLogoImageAsync(IFormFile logoImageFile)
        {
            if(logoImageFile.Length > 0)
            {
                return await UploadFileAsync(logoImageFile, "logos");
            }
            return null;
        }

        private string GetNewImageName(IFormFile formFile)
        {
            string imageName = new(Path.GetFileNameWithoutExtension(formFile.FileName).Replace(' ', '-'));
            imageName = imageName + DateTime.Now.ToString("yyyyMMdd'-'HHmmss") + Path.GetExtension(formFile.FileName);
            return imageName;
        }

        private async Task<string> UploadFileAsync(IFormFile formFile, string storageFolder)
        {
            string imageName = GetNewImageName(formFile);
            var stream = formFile.OpenReadStream();

            //Firebase uploading stuffs
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));
            var authLink = await authProvider.SignInWithEmailAndPasswordAsync(_authEmail, _authPassword);

            //Cancellation token
            var cancellation = new CancellationTokenSource();

            var upload = new FirebaseStorage(
                _bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                    ThrowOnCancel = true
                }
                )
                .Child("assets")
                .Child(storageFolder)
                .Child($"{imageName}")
                .PutAsync(stream, cancellation.Token);

            return await upload;
        }
    }
}

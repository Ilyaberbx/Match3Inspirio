using System.Threading.Tasks;
using UnityEngine;

namespace Inspirio.Global.Services.AssetsManagement
{
    public interface IAssetsService
    {
        Task<TAsset[]> LoadAll<TAsset>(string address) where TAsset : Object;
        Task<TAsset> Load<TAsset>(string address) where TAsset : Object;
    }
}
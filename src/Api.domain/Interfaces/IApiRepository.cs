using api.Domain.Entities.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Domain.Interfaces
{
    public interface IApiRepository
    {
        /// <summary>
        /// Get Operation : Returns List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">url of Get API</param>
        /// <returns></returns>
        public Task<List<T>> GetDataList<T>(string url, string token);

        /// <summary>
        /// GET operation: Returns Single Data.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<T> GetData<T>(string url, string token);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Return Object.</typeparam>
        /// <typeparam name="T1">Datatype to be passed in request body.</typeparam>
        /// <param name="url">url of post API.</param>
        /// <param name="data">Data to be passed in request body.</param>
        /// <param name="token">jwt token.</param>
        /// <returns></returns>
        public Task<T> PostData<T,T1>(string url, T1 data, string token);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Return Object.</typeparam>
        /// <typeparam name="T1">Datatype to be passed in request body.</typeparam>
        /// <param name="url">url of post API.</param>
        /// <param name="data">Data to be passed in request body.</param>
        /// <param name="token">jwt token.</param>
        /// <returns>return list of Return Data.</returns>
        public Task<List<T>> PostDataList<T, T1>(string url, List<T1> data, string token);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="token">jwt token.</param>
        /// <returns></returns>
        public Task<T> PostDataAndGetObj<T, T1>(string url, List<T1> data, string token);


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Return Object.</typeparam>
        /// <typeparam name="T1">Datatype to be passed in request body.</typeparam>
        /// <param name="url">url of post API.</param>
        /// <param name="data">Data to be passed in request body.</param>
        /// <param name="token">jwt token.</param>
        /// <returns></returns>
        public Task<T> PutData<T, T1>(string url, T1 data, string token);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Return Object.</typeparam>
        /// <typeparam name="T1">Datatype to be passed in request body.</typeparam>
        /// <param name="url">url of post API.</param>
        /// <param name="data">Data to be passed in request body.</param>
        /// <param name="token">Jwt token.</param>
        /// <returns>return list of Return Data.</returns>
        public Task<List<T>> PutDataList<T, T1>(string url, List<T1> data, string token);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<T> PutDataAndGetObj<T, T1>(string url, List<T1> data, string token);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="=token"></param>
        /// <returns></returns>
        public Task<T> DeleteData<T>(string url, string token);

    }
}

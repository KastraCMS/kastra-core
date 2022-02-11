/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kastra.Core.DTO;

namespace Kastra.Core.Services.Contracts
{
    public interface IFileManager
    {
        /// <summary>
        /// Add a file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="stream"></param>
        Task AddFileAsync(FileInfo file, System.IO.Stream stream);

        /// <summary>
        /// Get the binary of a file
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<byte[]> DownloadFileByGuidAsync(Guid fileId);
        
        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="fileId"></param>
        Task DeleteFileAsync(Guid fileId);
        
        /// <summary>
        /// Get file informations
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<FileInfo> GetFileAsync(Guid fileId);

        /// <summary>
        /// Get all files in a directory path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<IList<FileInfo>> GetFilesByPathAsync(string path);

        /// <summary>
        /// Scan a file and return true if the file is clean.
        /// </summary>
        /// <param name="fileBytes">File bytes</param>
        /// <returns>True if the file is clean</returns>
        Task<bool> ScanFileAsync(byte[] fileBytes);
    }
}
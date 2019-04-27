/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using Kastra.Core.Dto;

namespace Kastra.Core.Business
{
    public interface IFileManager
    {
        /// <summary>
        /// Add a file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="stream"></param>
        void AddFile(FileInfo file, System.IO.Stream stream);

        /// <summary>
        /// Get the binary of a file
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        byte[] DownloadFileByGuid(Guid fileId);
        
        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="fileId"></param>
        void DeleteFile(Guid fileId);
        
        /// <summary>
        /// Get file informations
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        FileInfo GetFile(Guid fileId);

        /// <summary>
        /// Get all files in a directory path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IList<FileInfo> GetFilesByPath(string path);
    }
}
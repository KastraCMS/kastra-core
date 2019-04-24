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
        void AddFile(FileInfo file, System.IO.Stream stream);

        void DownloadFileByGuid(Guid fileId);
        
        void DeleteFile(Guid fileId);
        
        FileInfo GetFile(Guid fileId);

        IList<FileInfo> GetFilesByPath(string path);
    }
}
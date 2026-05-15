import type { FileInfo } from "./types.ts";
import FileList from "./components/FileList.tsx";
import UploadFileForm from "./components/UploadFileForm.tsx";
import { useEffect, useState } from "react";

function App() {
  const [items, setItems] = useState<FileInfo[]>([]);

  const loadInfo = () => {
    GetFileList().then((data) => setItems(data));
  };

  useEffect(() => {
    loadInfo();
  }, []);

  return (
    <div>
      <UploadFileForm />
      <br></br>
      <input type="button" value="Load info" onClick={loadInfo} />
      <FileList items={items} />
    </div>
  );
}

function GetFileList(): Promise<FileInfo[]> {
  console.log("Fetching file list...");
  return fetch("/file")
    .then((response) => response.json())
    .then((data) =>
      Array.isArray(data) ? (data as FileInfo[]) : [data as FileInfo],
    );
}

export default App;

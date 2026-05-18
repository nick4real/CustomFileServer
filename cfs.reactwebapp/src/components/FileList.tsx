import type { FileInfo } from "../types.ts";

type FileListProps = {
  items: FileInfo[];
};

function FileList({ items }: FileListProps) {
  if (items.length === 0) {
    return <p>No items found.</p>;
  }

  return (
    <>
      <h1>Files</h1>
      <ul className="list-group">
        {items
          .slice()
          .reverse()
          .map((item) => (
            <li className="list-group-item" key={item.id}>
              <div>
                <strong>ID:</strong> {item.id}
              </div>
              <div>
                <strong>Name:</strong> {item.fileName}
              </div>
              <div>
                <strong>Type:</strong> {item.contentType}
              </div>
              <div>
                <strong>Size:</strong>
                {SizeUnitConverter({ sizeBytes: item.sizeBytes })}
              </div>
              <div>
                <strong>Uploaded:</strong>{" "}
                {new Date(item.uploadedAtUtc).toLocaleString()}
              </div>
              <button onClick={() => DownloadFile(item.id)}>Download</button>
            </li>
          ))}
      </ul>
    </>
  );
}

function SizeUnitConverter({ sizeBytes }: { sizeBytes: number }) {
  if (sizeBytes < 1024) {
    return <span>{sizeBytes} B</span>;
  } else if (sizeBytes < 1024 * 1024) {
    return <span>{(sizeBytes / 1024).toLocaleString()} KB</span>;
  }

  return <span>{(sizeBytes / (1024 * 1024)).toLocaleString()} MB</span>;
}

function DownloadFile(fileId: string) {
  const url = `/file/${fileId}`;
  window.open(url);
}

export default FileList;

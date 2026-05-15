import type { FileInfo } from "../types.ts";

type ListGroupProps = {
  items: FileInfo[];
};

function ListGroup({ items }: ListGroupProps) {
  if (items.length === 0) {
    return <p>No items found.</p>;
  }

  return (
    <>
      <h1>Files</h1>
      <ul className="list-group">
        {items.map((item) => (
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
              <strong>Size:</strong> {item.sizeBytes.toLocaleString()} bytes
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

function DownloadFile(fileId: string) {
  const url = `/file/${fileId}`;
  window.open(url);
}

export default ListGroup;

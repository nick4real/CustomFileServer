import React, { useState } from "react";

function UploadFileForm() {
  const [uploading, setUploading] = useState(false);
  const [message, setMessage] = useState<string | null>(null);

  async function handleSubmit(e: React.SyntheticEvent<HTMLFormElement>) {
    e.preventDefault();
    setMessage(null);
    const form = e.currentTarget;
    const input = form.querySelector(
      'input[type="file"]',
    ) as HTMLInputElement | null;
    if (!input || !input.files || input.files.length === 0) {
      setMessage("Please choose a file to upload.");
      return;
    }

    const fd = new FormData();
    fd.append("file", input.files[0]);

    setUploading(true);
    try {
      const res = await fetch("/file", { method: "POST", body: fd });
      if (!res.ok) throw new Error(`${res.status} ${res.statusText}`);
      const json = await res.json().catch(() => null);
      setMessage((json && json.message) || "Upload successful");
    } catch (err) {
      setMessage("Upload failed");
    } finally {
      setUploading(false);
      if (input) input.value = "";
    }
  }

  return (
    <form onSubmit={handleSubmit} encType="multipart/form-data">
      <input type="file" name="file" />
      <button type="submit" disabled={uploading}>
        {uploading ? "Uploading..." : "Upload"}
      </button>
      {message && <div role="status">{message}</div>}
    </form>
  );
}

export default UploadFileForm;

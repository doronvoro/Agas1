function downloadFile(array, fileName, mimeType) {
    const blob = new Blob([new Uint8Array(array)], { type: mimeType });
    const link = document.createElement("a");
    link.href = URL.createObjectURL(blob);
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

// App.js
import React, { useState, useCallback } from "react";
import ReactDOM from "react-dom/client";
// import ReceiveTextFromUnity from "./ReceiveTextFromUnityButton";
import UnityLoader from "./UnityLoader";
// import SentTextToUnityButton from "./SentTextToUnityButton";
// import SpawnObjectButton from "./SpawnObjectButton";

const App = () => {
  const [iframeWindow, setIframeWindow] = useState(null);

  const handleIframeLoad = useCallback((iframeWindow) => {
    setIframeWindow(iframeWindow);
  }, []);

  return (
    <div>
      <UnityLoader onIframeLoad={handleIframeLoad} />
    </div>
  );
};

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(<App />);

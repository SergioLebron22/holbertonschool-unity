// UnityLoader.js
import React, { useEffect } from "react";

const UnityLoader = ({ onIframeLoad }) => {
  useEffect(() => {
    const iframe = document.createElement("iframe");
    iframe.src = "BuildOutput/index.html"; // Replace with the actual path to your HTML file
    iframe.style.width = "100%";
    iframe.style.height = "100%";
    iframe.style.border = "none";

    // Append iframe to the parent container instead of document.body
    const container = document.getElementById("gameLoaderContainer");
    if (container) {
      container.appendChild(iframe);
    }

    const handleLoad = () => {
      const iframeWindow = iframe.contentWindow;
      if (onIframeLoad) {
        onIframeLoad(iframeWindow);
      }

      const checkUnityInstance = setInterval(function () {
        if (
          typeof iframeWindow.unityInstance !== "undefined" &&
          iframeWindow.unityInstance !== null
        ) {
          console.log("Unity Instance Loaded:", iframeWindow.unityInstance);
          clearInterval(checkUnityInstance);
        }
      }, 100);
    };

    iframe.addEventListener("load", handleLoad);

    return () => {
      iframe.removeEventListener("load", handleLoad);
      if (container) {
        container.removeChild(iframe);
      }
    };
  }, [onIframeLoad]);

  return null;
};

export default UnityLoader;

// UnityLoader.js
import React, { useEffect } from "react";

const UnityLoader = ({ onIframeLoad }) => {
  useEffect(() => {
    const iframe = document.createElement("iframe");
    iframe.src = "BuildOutput/index.html"; // Replace with the actual path to your HTML file
    iframe.style.width = "960px";
    iframe.style.height = "660px";
    iframe.style.border = "none";
    iframe.style.position = "absolute";
    iframe.style.top = "0";
    iframe.style.left = "0";

    document.body.appendChild(iframe);

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
      document.body.removeChild(iframe);
    };
  }, [onIframeLoad]); // Add onIframeLoad to the dependency array

  return null;
};

export default UnityLoader;

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

  const colors = {
    primary: '#1a1a2e',           // Dark blue/purple
    secondary: '#16213e',         // Darker blue
    accent: '#00ffff',           // Medium blue
    highlight: '#00ffff',        // Light blue
    text: '#ffffff',             // White text
    textSecondary: '#b0b0b0',    // Light gray text
    background: '#0d1117',       // Very dark background
    cardBackground: '#161b22',   // Dark card background
    border: '#30363d',           // Border color
    button: '#238636',           // Green button
    buttonHover: '#2ea043',      // Green button hover
  };

  const fonts = {
    primary: '"Segoe UI", Tahoma, Geneva, Verdana, sans-serif',
    heading: '"Arial Black", Arial, sans-serif',
    mono: 'Monaco, "Lucida Console", monospace',
  };

  const spacing = {
    xs: '0.5rem',
    sm: '1rem',
    md: '1.5rem',
    lg: '2rem',
    xl: '3rem',
    xxl: '4rem',
  };

  const gameInfo = {
    title: "Your WebXR Game Title",
    description: "Experience immersive virtual reality directly in your browser. This WebXR game pushes the boundaries of what's possible on the web, delivering console-quality graphics and gameplay.",
    features: [
      "Full WebXR support for VR headsets",
      "Cross-platform compatibility",
      "Immersive 3D environments",
      "Intuitive VR controls",
      "High-performance Unity engine"
    ],
    requirements: "WebXR compatible browser (Chrome, Firefox, Edge) and optional VR headset"
  };

  const developerInfo = {
    name: "Your Studio Name",
    description: "We're passionate game developers creating cutting-edge WebXR experiences that bring virtual reality to everyone.",
    contact: "contact@yourstudio.com",
    website: "www.yourstudio.com"
  };

  return (
    <div 
      style={{
        minHeight: '100vh',
        backgroundColor: colors.background,
        color: colors.text,
        fontFamily: fonts.primary,
        lineHeight: '1.6',
      }}
    >
      {/* ============ HEADER ============ */}
      <header 
        style={{
          backgroundColor: colors.primary,
          padding: spacing.md,
          borderBottom: `2px solid ${colors.border}`,
          position: 'sticky',
          top: 0,
          zIndex: 1000,
          backdropFilter: 'blur(10px)',
        }}
      >
        <div style={{ maxWidth: '1200px', margin: '0 auto' }}>
          {/* Logo Placeholder */}
          <div 
            style={{
              width: '200px',
              height: '60px',
              backgroundColor: colors.accent,
              border: `2px dashed ${colors.highlight}`,
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              borderRadius: '8px',
              fontFamily: fonts.mono,
              fontSize: '14px',
              color: colors.textSecondary,
            }}
          >
            LOGO PLACEHOLDER
          </div>
        </div>
      </header>

      {/* ============ MAIN CONTENT ============ */}
      <main style={{ maxWidth: '1200px', margin: '0 auto', padding: spacing.lg }}>
        
        {/* Game Title */}
        <h1 
          style={{
            fontSize: '3rem',
            fontFamily: fonts.heading,
            textAlign: 'center',
            marginBottom: spacing.lg,
            background: `linear-gradient(45deg, ${colors.highlight}, ${colors.accent})`,
            WebkitBackgroundClip: 'text',
            WebkitTextFillColor: 'transparent',
            backgroundClip: 'text',
          }}
        >
          {gameInfo.title}
        </h1>

        {/* Game Loader Section */}
        <section 
          style={{
            backgroundColor: colors.cardBackground,
            border: `1px solid ${colors.border}`,
            borderRadius: '12px',
            padding: spacing.xl,
            marginBottom: spacing.xl,
            textAlign: 'center',
          }}
        >
          <h2 
            style={{
              fontSize: '1.5rem',
              marginBottom: spacing.md,
              color: colors.highlight,
            }}
          >
            Game Loader
          </h2>
          
          <div 
            id="gameLoaderContainer" 
            style={{
              width: '100%',
              height: '660px', // Match the iframe height
              position: 'relative',
              overflow: 'hidden',
              color: colors.text,
            }}
          >
            <UnityLoader onIframeLoad={handleIframeLoad} />
          </div>
        </section>

        {/* Game Info Section */}
        <section 
          style={{
            backgroundColor: colors.cardBackground,
            border: `1px solid ${colors.border}`,
            borderRadius: '12px',
            padding: spacing.xl,
            marginBottom: spacing.xl,
          }}
        >
          <h2 
            style={{
              fontSize: '2rem',
              marginBottom: spacing.md,
              color: colors.highlight,
              fontFamily: fonts.heading,
            }}
          >
            About the Game
          </h2>
          
          <p 
            style={{
              fontSize: '1.1rem',
              marginBottom: spacing.lg,
              color: colors.textSecondary,
            }}
          >
            {gameInfo.description}
          </p>

          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: spacing.lg }}>
            {/* Features */}
            <div>
              <h3 
                style={{
                  fontSize: '1.3rem',
                  marginBottom: spacing.sm,
                  color: colors.text,
                }}
              >
                Features
              </h3>
              <ul style={{ listStyle: 'none', padding: 0 }}>
                {gameInfo.features.map((feature, index) => (
                  <li 
                    key={index}
                    style={{
                      padding: spacing.xs,
                      marginBottom: spacing.xs,
                      backgroundColor: colors.secondary,
                      borderRadius: '4px',
                      borderLeft: `4px solid ${colors.accent}`,
                    }}
                  >
                    {feature}
                  </li>
                ))}
              </ul>
            </div>

            {/* Requirements */}
            <div>
              <h3 
                style={{
                  fontSize: '1.3rem',
                  marginBottom: spacing.sm,
                  color: colors.text,
                }}
              >
                System Requirements
              </h3>
              <p 
                style={{
                  padding: spacing.md,
                  backgroundColor: colors.secondary,
                  borderRadius: '8px',
                  border: `1px solid ${colors.border}`,
                }}
              >
                {gameInfo.requirements}
              </p>
            </div>
          </div>
        </section>

        {/* Developer Info Section */}
        <section 
          style={{
            backgroundColor: colors.cardBackground,
            border: `1px solid ${colors.border}`,
            borderRadius: '12px',
            padding: spacing.xl,
            marginBottom: spacing.xl,
          }}
        >
          <h2 
            style={{
              fontSize: '2rem',
              marginBottom: spacing.md,
              color: colors.highlight,
              fontFamily: fonts.heading,
            }}
          >
            About the Developer
          </h2>
          
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(250px, 1fr))', gap: spacing.lg }}>
            <div>
              <h3 
                style={{
                  fontSize: '1.3rem',
                  marginBottom: spacing.sm,
                  color: colors.text,
                }}
              >
                {developerInfo.name}
              </h3>
              <p style={{ color: colors.textSecondary, marginBottom: spacing.md }}>
                {developerInfo.description}
              </p>
            </div>
            
            <div>
              <h4 
                style={{
                  fontSize: '1.1rem',
                  marginBottom: spacing.sm,
                  color: colors.text,
                }}
              >
                Contact Information
              </h4>
              <p style={{ color: colors.textSecondary, marginBottom: spacing.xs }}>
                Email: {developerInfo.contact}
              </p>
              <p style={{ color: colors.textSecondary }}>
                Website: {developerInfo.website}
              </p>
            </div>
          </div>
        </section>
      </main>

      {/* ============ FOOTER ============ */}
      <footer 
        style={{
          backgroundColor: colors.primary,
          borderTop: `2px solid ${colors.border}`,
          padding: spacing.lg,
          marginTop: spacing.xxl,
        }}
      >
        <div 
          style={{
            maxWidth: '1200px',
            margin: '0 auto',
            textAlign: 'center',
          }}
        >
          <p 
            style={{
              color: colors.textSecondary,
              fontSize: '0.9rem',
              marginBottom: spacing.sm,
            }}
          >
            © 2024 {developerInfo.name}. All rights reserved.
          </p>
          <p 
            style={{
              color: colors.textSecondary,
              fontSize: '0.8rem',
              opacity: 0.7,
            }}
          >
            Built with React and Unity WebXR
          </p>
        </div>
      </footer>
    </div>
    // <div>
    //   <UnityLoader onIframeLoad={handleIframeLoad} />
    // </div>
  );
};

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(<App />);

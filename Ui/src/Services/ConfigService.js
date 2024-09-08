let config = null;

export const loadConfig = async () => {
  if (!config) {
    const response = await fetch('/config.json');
    if (!response.ok) throw new Error('Failed to load configuration');
    config = await response.json();
  }
  return config;
};

export const getConfig = () => {
  if (!config) throw new Error('Configuration not loaded');
  return config;
};

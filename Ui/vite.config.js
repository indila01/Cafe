import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { TanStackRouterVite } from '@tanstack/router-plugin/vite'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    react(),
    TanStackRouterVite()],
  server: {
    host: true,
    port: 3000, // This is the port which we will use in docker
  
     watch: {
       usePolling: true
     }
  }
})

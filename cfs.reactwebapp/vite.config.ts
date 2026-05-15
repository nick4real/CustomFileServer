import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    server: {
        port: 60491,
        proxy: {
            '/file': {
                target: 'https://127.0.0.1:7983',
                changeOrigin: true,
                secure: false,
            },
        },
    }
})

import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vitest/config';

export default defineConfig({
	plugins: [sveltekit()],
	test: {
		include: ['src/**/*.{test,spec}.{js,ts}']
	},
	server: {
		proxy: {
			'/chathub': {
				target: 'http://localhost:5073',
				changeOrigin: true,
				ws: true,
			},
		},
	}
});

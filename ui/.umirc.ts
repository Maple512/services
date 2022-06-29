import { defineConfig } from 'umi';

export default defineConfig({
  layout: {
    name: 'Hub',
  },
  nodeModulesTransform: {
    type: 'none',
  },
  routes: [{ path: '/', component: '@/pages/index' }],
  fastRefresh: {},
});

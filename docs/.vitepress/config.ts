import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "My Awesome Project",
    description: "A VitePress Site",
  base: '/DataVaultDotNet',
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: 'Home', link: '/' },
      { text: 'Guide', link: '/guide/' },
      { text: 'ADRs', link: '/adrs/' },
      { text: 'BDD', link: '/bdd/' }
    ],

    sidebar:
      {
            '/guide/': [
                { text: 'Getting Started', link: '/guide/getting-started' },
                { text: 'Pipeline DSL', link: '/guide/pipeline-dsl' }
            ],
            '/adrs/': [
                { text: 'Overview', link: '/adrs/' }
            ],
            '/bdd/': [
                { text: 'Example Scenarios', link: '/bdd/' }
            ]
      },

    socialLinks: [
      { icon: 'github', link: 'https://github.com/vuejs/vitepress' }
    ]
  }
})

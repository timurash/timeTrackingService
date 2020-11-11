<template>
  <div class="app-wrapper">
    <navbar></navbar>
    <div class="main-container">
      <app-content></app-content>
    </div>
  </div>
</template>

<script>
import { Navbar, AppContent } from './components'

export default {
  name: 'AppLayout',
  components: {
    Navbar,
    AppContent
  }
}
</script>
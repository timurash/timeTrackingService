<template>
  <div class="app-wrapper">
    <navbar></navbar>
    <div class="main-container">
      <section class="app-container">
        <router-view></router-view>
      </section>
    </div>
  </div>
</template>

<script>
import {Navbar} from './components'

export default {
  name: 'AppLayout',
  components: {
    Navbar
  }
}
</script>
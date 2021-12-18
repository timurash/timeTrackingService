<template>
    <div class="app-wrapper">
        <NavBar />
        <div class="main-container">
            <section class="app-container">
                <router-view />
            </section>
        </div>
    </div>
</template>

<script>
import NavBar from '@/components/NavBar/NavBar.vue';

export default {
    name: 'AppLayout',
    components: {
        NavBar,
    },
};
</script>

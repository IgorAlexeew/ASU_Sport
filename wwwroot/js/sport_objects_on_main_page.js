function choose(choices) {
    let index = Math.floor(Math.random() * choices.length);
    return choices[index];
}

const sportObjectsApp = {
    data() {
        return {
            sportObjects: []
        }
    },
    mounted() {
        this.sportObjects = [
            {
                objectName: "Спортивный зал",
                days: null
            },
            {
                objectName: "Спортивная площадка",
                days: null
            },
            {
                objectName: "Бассейн",
                days: null
            }
        ]
    }
}

Vue.createApp(sportObjectsApp).mount("#sport-objects-view");
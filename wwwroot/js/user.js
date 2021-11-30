const app = Vue.createApp({
    data() {
        return {
            user: {
                last_name: "Достоевский",
                first_name: "Федор",
                middle_name: "Михайлович",
                birth_date: "11 ноября 1821 г.",
                phone_number: "8(818)211-11-11",
                events: [
                    {
                        name: "Свободное плавание",
                        date: "21.12.2021",
                        time: "12:00"
                    },
                    {
                        name: "Свободное плавание",
                        date: "21.12.2021",
                        time: "12:00"
                    },
                    {
                        name: "Базовая тренировка",
                        date: "21.12.2021",
                        time: "12:00"
                    }
                ]
            }
        }
    }
});

app.component("user-info",{
    props: ['user'],
    template: `
        <div class="user">
            <img src="/img/users/user.svg" class="photo" alt=""/>
            <div class="data">
                <p class="full-name">{{ user.last_name }} {{ user.first_name }} {{ user.middle_name }}</p>
                <p class="birth-date">{{ user.birth_date }}</p>
    <!--            <p class="e-mail">dostoevskiy@poet.ru</p>-->
                <p class="phone-number">{{ user.phone_number }}</p>
            </div>
            <a href="#" class="logout">
                <svg xmlns="http://www.w3.org/2000/svg" width="85" height="84" viewBox="0 0 85 84" fill="none">
                    <path d="M0 9.33333C0 4.2 4.2 0 9.33333 0H46.6667V9.33333H9.33333V74.6667H46.6667V84H9.33333C4.2 84 0 79.8 0 74.6667V9.33333ZM66.1547 37.3333L54.32 25.4987L60.9187 18.9L84.0187 42L60.9187 65.1L54.32 58.5013L66.1547 46.6667H35.42V37.3333H66.1547Z"/>
                </svg>
            </a>
        </div>
    `
}
)

app.component("nearest-events",{
    props: ['user'],
    template: `
        <div class="nearest-events">
            <div class="title">Предстоящие занятия:</div>
            <div class="events">
                <a v-for="event in user.events" href="#" class="event blue">
                    <p class="name">{{ event.name }}</p>
                    <p class="date">{{ event.date }}</p>
                    <p class="time">{{ event.time }}</p>
                </a>
            </div>
        </div>
  `
})
app.mount("#app")
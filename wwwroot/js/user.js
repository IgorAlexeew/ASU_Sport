const app = Vue.createApp({
    data() {
        return {
            user: {
                firstName: null,
                middleName: null,
                lastName: null,
                phoneNumber: null,
                dateOfBirth: null,
                events: [
                    {
                        section: {
                            sectionName: null,
                            duration: null
                        },
                        trainer: {
                            firstName: null,
                            middleName: null,
                            lastName: null
                        },
                        time: null
                    }
                ]
            }
        }
    },
    beforeCreate() {
        axios
            .get("https://localhost:5001/api/user/get-user-info")
            .then(response => this.user = response.data)
            .catch(error => console.log(error))
    }
});

app.component("user-info",{
    props: ['user'],
    template: `
        <div class="user">
            <img src="/img/users/user.svg" class="photo" alt=""/>
            <div class="data">
                <p class="full-name">{{ user?.lastName }} {{ user?.firstName }} {{ user?.middleName }}</p>
                <p class="birth-date">Дата рождения: {{ user?.dateOfBirth }}</p>
    <!--            <p class="e-mail">dostoevskiy@poet.ru</p>-->
                <p class="phone-number">Номер телефона: {{ user?.phoneNumber }}</p>
            </div>
            <a href="https://localhost:5001/api/auth/logout" class="logout">
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
                <template v-if="user.events !== null && user.events.length > 0">
                  <a v-for="event in user?.events" href="#" class="event blue">
                    <p class="name">{{ event?.section?.sectionName }}</p>
                    <p class="date">{{ event?.time }}</p>
                    <!--                    <p class="time">{{ event.time }}</p>-->
                  </a>
                </template>
              <p class="empty" v-else>Нет предстоящих событий</p>
            </div>
        </div>
  `
})
app.mount("#app")
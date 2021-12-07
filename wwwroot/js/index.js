const app = Vue.createApp({
    data() {
        return {
            sportObjects: [],
            site_routing: {
                nav_links: {
                    objects: { name: "Объекты", url: "/", is_selected: false},
                    news: { name: "Новости", url: "/news", is_selected: false},
                    contacts: { name: "Контакты", url: "/contacts", is_selected: false}
                },
                auth_links: {
                    sign_in_url: "/login",
                    sign_up_url: "/registration"
                }
            },
            short_form: true,
            objects_count_to_show: 3
        }
    },
    computed: {
        objects_to_show() {
            console.log(this.objects_count_to_show)
            return this.sportObjects.slice(0, this.objects_count_to_show);
        }
    },
    methods: {
        toggle_objects_view() {
            this.short_form = !this.short_form
            if (!this.short_form) {
                this.objects_count_to_show = this.sportObjects.length
                console.log("changed " + this.objects_count_to_show)
            }
            else {
                this.objects_count_to_show = 3
            }
        }
    },
    mounted() {
        let path = window.location.pathname
        for (let key in this.site_routing.nav_links) {
            let link = this.site_routing.nav_links[key]
            link.is_selected = (path === link.url)
        }
        /*this.sportObjects = [
            {
                "objectName": "Бассейн",
                "workingHours": "07:00 - 22:00",
                "days": null,
                "serviceName": "Разовое занятие",
                "price": 200
            },
            {
                "objectName": "Мини футбольное поле",
                "workingHours": null,
                "days": null,
                "serviceName": "Разовое занятие",
                "price": 600
            },
            {
                "objectName": "Тренажерный зал",
                "workingHours": "09:00 - 21:00",
                "days": null,
                "serviceName": "Разовое занятие",
                "price": 100
            },
            {
                "objectName": "Тренажерный зал",
                "workingHours": "09:00 - 21:00",
                "days": null,
                "serviceName": "Разовое занятие",
                "price": 100
            },
            {
                "objectName": "Мини футбольное поле",
                "workingHours": null,
                "days": null,
                "serviceName": "Разовое занятие",
                "price": 600
            },
            {
                "objectName": "Тренажерный зал",
                "workingHours": "09:00 - 21:00",
                "days": null,
                "serviceName": "Разовое занятие",
                "price": 100
            },
            {
                "objectName": "Тренажерный зал",
                "workingHours": "09:00 - 21:00",
                "days": null,
                "serviceName": "Разовое занятие",
                "price": 100
            }
        ]*/
        axios
            .get("https://localhost:5001/api/sport-object/get-info")
            .then(response => {this.sportObjects = response.data; })
            .catch(error => console.log(error));

    }
})

app.component("default-header",{
    props: ['routing'],
    template:`
        <div class="header">
            <div class="logo">
                <img src="/img/asu-clr.png" alt="Астраханский Государственный Университет" id="asu-logo">
                <div class="description">
                    Астраханский<br>
                    государственный<br>
                    университет<br>
                </div>
            </div>
            <div class="nav">
                <ul>
                    <li v-for="link in routing.nav_links"><a :class="{ selected: link.is_selected }" :href="link.url">{{ link.name }}</a></li>
                </ul>
            </div>
            <div class="auth">
                <a :href="routing.auth_links.sign_in_url" id="sign-in">войти</a>
                <a :href="routing.auth_links.sign_up_url" id="sign-up">зарегистрироваться</a>
            </div>
        </div>
    `
})

app.component("sport-object",
    {
        props: ['sport_object'],
        methods: {
            toggle_objects_view() {
                this.$emit("toggle_objects_view");
            }
        },
        template: `
            <div @toggle_objects_view="this.toggle_objects_view" class="object">
                <div class="title">{{ sport_object.objectName }}</div>
                <div class="time-range">{{ sport_object.workingHours }}</div>
                <div class="point days">
                    <div class="label">Наименее загруженные дни:</div>
                    <div v-if="sport_object.days" v-for="day in sport_object?.days" class="days-list">
                        <div class="day">{{ day?.name }}</div>
                    </div>
                    <div class="days-list" v-else>
                        <div class="day null">нет данных</div>
                    </div>
                </div>
                <div class="point average-cost">
                    <div class="label">Стоимость занятия:</div>
                    <div class="cost">от {{ sport_object?.price }} ₽</div>
                </div>
            </div>
        `
    }
)

function bindScroll() {
    let element = $("#sport-objects-view");
    console.log(element)
    element.on('wheel', (event) => {
        event.preventDefault();
        let delta = Math.max(-1, Math.min(1, (event.originalEvent.wheelDelta || -event.originalEvent.detail)));

        element.scrollLeft( element.scrollLeft() - ( delta * 40 ) );
    });
}

app.component("sport-objects-view",{
    props: ['sport_objects'],
    mounted() {
        bindScroll();
    },
    template: `
      <div id="sport-objects-view" class="objects" :class="{ wide: !this.$parent.short_form }">
          <a v-show="!this.$parent.short_form" href="#" class="more-objects" @click='$emit("toggle_objects_view")'>
            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" width="1em" height="1em" preserveAspectRatio="xMidYMid meet" viewBox="0 0 1024 1024">
              <path d="M685.248 104.704a64 64 0 0 1 0 90.496L368.448 512l316.8 316.8a64 64 0 0 1-90.496 90.496L232.704 557.248a64 64 0 0 1 0-90.496l362.048-362.048a64 64 0 0 1 90.496 0z"/>
            </svg>
          </a>
          <sport-object v-for="sportObject in sport_objects" :sport_object="sportObject"></sport-object>
          <a v-show="this.$parent.short_form" href="#" class="more-objects" @click='$emit("toggle_objects_view")'>
            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img"
                 width="1em" height="1em" preserveAspectRatio="xMidYMid meet" viewBox="0 0 1024 1024">
              <path
                  d="M338.752 104.704a64 64 0 0 0 0 90.496l316.8 316.8l-316.8 316.8a64 64 0 0 0 90.496 90.496l362.048-362.048a64 64 0 0 0 0-90.496L429.248 104.704a64 64 0 0 0-90.496 0z"/>
            </svg>
          </a>
      </div>
    `
})
app.mount("#app")

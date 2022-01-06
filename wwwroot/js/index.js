const app = Vue.createApp({
    data() {
        return {
            sportObjects: [], // список спортивных объектов
            short_form: true, // флаг формата текущего отображения
            objects_count_to_show: 3 // кол-во объектов для отображеня
        }
    },
    computed: {
        // объекты для отображения на странице
        objects_to_show() {
            if (!this.short_form) {
                this.objects_count_to_show = this.sportObjects.length
            }
            else {
                this.objects_count_to_show = 3
            }
            return this.sportObjects.slice(0, this.objects_count_to_show);
        }
    },
    methods: {
        // переключения вида
        toggle_objects_view() {
            this.short_form = !this.short_form
        }
    },
    components: {'default-header': header_component}, // добавление header_component в приложение
    mounted() {
        // получение списка спортивных объектов
        axios
            .get("/api/sport-object/get-info")
            .then(response => {this.sportObjects = response.data; })
            .catch(error => console.log(error));
    }
})

/* Компонент Блок с информацией о спортивном объекте */
app.component("sport-object",
    {
        data() {
            return {
                day_date_options: {
                    weekday: "short"
                }
            }
        },
        props: ['sport_object'],
        template: `
            <div class="object">
                <a :href="'/events?id=' + sport_object.id" class="title">{{ sport_object.objectName }}</a>
                <div class="time-range">{{ sport_object.workingHours }}</div>
                <div class="point days">
                    <div class="label">Наименее загруженные дни:</div>
                    <div v-if="sport_object.days" v-for="day in sport_object?.days" class="days-list">
                        <a :href="'events?id=' + sport_object.id + '&date=' + day" class="day">{{ (new Date(day)).toLocaleDateString("ru", this.day_date_options) }}</a>
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

/* Область отображения спортивных объектов*/
app.component("sport-objects-view",{
    props: [],
    template: `
      <div id="sport-objects-view" class="objects" :class="{ wide: !this.$root.short_form }">
          <a v-show="!this.$root.short_form" href="#" class="more-objects" @click='this.$root.toggle_objects_view()'>
            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" width="1em" height="1em" preserveAspectRatio="xMidYMid meet" viewBox="0 0 1024 1024">
              <path d="M685.248 104.704a64 64 0 0 1 0 90.496L368.448 512l316.8 316.8a64 64 0 0 1-90.496 90.496L232.704 557.248a64 64 0 0 1 0-90.496l362.048-362.048a64 64 0 0 1 90.496 0z"/>
            </svg>
          </a>
          <sport-object v-if="this.$root.objects_to_show.length > 0" v-for="sportObject in this.$root.objects_to_show" :sport_object="sportObject"></sport-object>
          <p style="color: #fff" v-else>Загрузка...</p>
          <a v-show="this.$root.short_form && this.$root.objects_to_show.length > 0" href="#" class="more-objects" @click='this.$root.toggle_objects_view()'>
            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img"
                 width="1em" height="1em" preserveAspectRatio="xMidYMid meet" viewBox="0 0 1024 1024">
              <path
                  d="M338.752 104.704a64 64 0 0 0 0 90.496l316.8 316.8l-316.8 316.8a64 64 0 0 0 90.496 90.496l362.048-362.048a64 64 0 0 0 0-90.496L429.248 104.704a64 64 0 0 0-90.496 0z"/>
            </svg>
          </a>
      </div>
    `
})

/* Компонент основной страницы */
app.component('main-page', {
    props: [],
    template: `
        <div class="main-page-block">
            <div class="brief" :class="{ 'none-display': !this.$root.short_form }">
                <div class="title">СПОРТ</div>
                <div class="description">
                    Данный ресурс предоставляет  информацию о спортивных объектах АГУ,
                    мероприятиях с ними связанных, а также дает возможность осуществлять
                    бронирование тех или иных объектов на интересующее Вас время
                </div>
            </div>
            <sport-objects-view :sport_objects="this.$root.objects_to_show"></sport-objects-view>
        </div>
    `
})

const root = app.mount("#app")


/* Добавление горизонтальной прокрутки */
let element = $("#sport-objects-view")
console.log(element)
element.on('wheel', (event) => {
    event.preventDefault();
    let delta = Math.max(-1, Math.min(1, (event.originalEvent.wheelDelta || -event.originalEvent.detail)));

    element.scrollLeft( element.scrollLeft() - ( delta * 40 ) );
});